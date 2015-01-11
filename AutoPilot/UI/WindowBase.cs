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

		protected GUIStyle closeButtonStyle;

		protected WindowBase(string windowTitle)
		{
			this.windowTitle = windowTitle;
			this.windowId = windowTitle.GetHashCode() + new System.Random().Next(65536);

			windowPos = new Rect(60, 60, 60, 60);
			visible = false;
		}

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

		public void SetSize(int width, int height)
		{
			windowPos.width = width;
			windowPos.height = height;
		}

		public virtual void Load(ConfigNode config)
		{
			// Not implemented
		}

		public virtual void Save(ConfigNode config)
		{
			// Not implemented
		}
			
		protected virtual void DrawWindow()
		{
			if (visible)
			{
				bool paused = false;
				if (HighLogic.LoadedSceneIsFlight)
				{
					try
					{
						paused = PauseMenu.isOpen || FlightResultsDialog.isDisplaying;
					}
					catch (Exception)
					{
						// ignore the error and assume the pause menu is not open
					}
				}

				if (!paused)
				{
					GUI.skin = HighLogic.Skin;
					ConfigureStyles();

					windowPos = APUtils.EnsureVisible(windowPos);
					windowPos = GUILayout.Window(windowId, windowPos, PreDrawWindowContents, windowTitle, GUILayout.ExpandWidth(false),
						GUILayout.ExpandHeight(true), GUILayout.MinWidth(64), GUILayout.MinHeight(64));
				}
			}
		}

		protected virtual void ConfigureStyles()
		{
			if (closeButtonStyle == null)
			{
				closeButtonStyle = new GUIStyle(GUI.skin.button);
				closeButtonStyle.padding = new RectOffset(5, 5, 3, 0);
				closeButtonStyle.margin = new RectOffset(1, 1, 1, 1);
				closeButtonStyle.stretchWidth = false;
				closeButtonStyle.stretchHeight = false;
				closeButtonStyle.alignment = TextAnchor.MiddleCenter;
			}
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

		protected abstract void DrawWindowContents(int windowId);
	}
}
