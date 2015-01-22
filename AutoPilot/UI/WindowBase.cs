using System;
using UnityEngine;

namespace AutoPilot.UI
{
	public abstract class WindowBase
	{
		private string windowTitle;
		private int windowId;

		protected Rect windowPos;
		private bool visible;

		private bool stylesLoaded = false;
		protected GUIStyle closeButtonStyle, 
			nameStyle, 
			valueStyle,
			textFieldStyle;

		protected WindowBase(string windowTitle)
		{
			this.windowTitle = windowTitle;
			this.windowId = windowTitle.GetHashCode() + new System.Random().Next(65536);

			windowPos = new Rect(60, 60, 60, 60);
			visible = false;
		}

		#region Visibility
		public bool IsVisible()
		{
			return visible;
		}

		public virtual void SetVisible(bool newValue)
		{
			if (newValue)
			{
				if (!visible)
				{
					RenderingManager.AddToPostDrawQueue(3, new Callback(DrawWindow));
				}
			}
			else
			{
				if (visible)
				{
					RenderingManager.RemoveFromPostDrawQueue(3, new Callback(DrawWindow));
				}
			}

			this.visible = newValue;
		}

		public void ToggleVisible()
		{
			SetVisible(!visible);
		}

		#endregion

		public void SetSize(int width, int height)
		{
			windowPos.width = width;
			windowPos.height = height;
		}
			
		protected virtual void DrawWindow()
		{
			if (visible && ! APUtils.IsPaused ())
			{
				GUI.skin = HighLogic.Skin;
				ConfigureStyles();

				windowPos = APUtils.EnsureVisible(windowPos);
				windowPos = GUILayout.Window(windowId, windowPos, PreDrawWindowContents, windowTitle, GUILayout.ExpandWidth(false),
					GUILayout.ExpandHeight(true), GUILayout.MinWidth(64), GUILayout.MinHeight(64));
			}
		}

		private void ConfigureStyles()
		{
			if (stylesLoaded)
				return;

			closeButtonStyle = new GUIStyle (HighLogic.Skin.button) {
				margin = new RectOffset (1, 1, 1, 1),
				padding = new RectOffset (5, 5, 3, 0),
				alignment = TextAnchor.MiddleCenter,
				stretchWidth = false,
				stretchHeight = false
			};

			nameStyle = new GUIStyle (HighLogic.Skin.label) {
				normal = {
					textColor = Color.white
				},
				margin = new RectOffset (),
				padding = new RectOffset (5, 0, 0, 0),
				alignment = TextAnchor.MiddleLeft,
				fontSize = (int)(11),
				fontStyle = FontStyle.Bold,
				fixedHeight = 20.0f
			};

			valueStyle = new GUIStyle (HighLogic.Skin.label) {
				margin = new RectOffset (),
				padding = new RectOffset (0, 5, 0, 0),
				alignment = TextAnchor.MiddleRight,
				fontSize = (int)(11),
				fontStyle = FontStyle.Normal,
				fixedHeight = 20.0f 
			};

			textFieldStyle = new GUIStyle (HighLogic.Skin.textField) {
				normal = {
					textColor = Color.white
				},
				margin = new RectOffset (),
				padding = new RectOffset (5, 0, 0, 0),
				alignment = TextAnchor.MiddleLeft,
				fontSize = (int)(11),
				fontStyle = FontStyle.Bold,
				fixedHeight = 20.0f

			};

			stylesLoaded = true;
		}

		private void PreDrawWindowContents(int windowId)
		{
			DrawWindowContents(windowId);

			if (GUI.Button(new Rect(windowPos.width - 24, 4, 20, 20), "X", closeButtonStyle))
			{
				SetVisible(false);
			}

			GUI.DragWindow();
		}

		#region Draw Methods

		protected void DrawNameValueLine(string label, string value, float width = 300f) 
		{
			GUILayout.BeginHorizontal(GUILayout.Width(width));

			GUILayout.Label (label, nameStyle);
			GUILayout.FlexibleSpace ();
			GUILayout.TextArea (value, valueStyle);

			GUILayout.EndHorizontal (); 
		}

		protected void DrawNameTextBox(string leftLabel, string defValue, Action<String> onUpdate, float width = 300f) 
		{
			GUILayout.BeginHorizontal(GUILayout.Width(width));

			GUILayout.Label(leftLabel, nameStyle);
			GUILayout.FlexibleSpace ();
			string result = GUILayout.TextField(defValue, textFieldStyle);

			GUILayout.EndHorizontal();

			onUpdate (result);
		}

		#endregion

		protected abstract void DrawWindowContents(int windowId);
	}
}
