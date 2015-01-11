using System;
using UnityEngine;

namespace AutoPilot.UI
{
	public class APStatusWindow : WindowBase
	{
		private APFlightControl flightControl;

		public GUIStyle NameStyle { get; set; }

		public GUIStyle ValueStyle { get; set; }

		private const float FixedWidth = 300f;

		public APStatusWindow (APFlightControl flightControl) : base("AP Status")
		{
			// Force default size
			this.flightControl = flightControl;

			windowPos = new Rect(60, 60, 230, 100);

			InitialiseStyles ();
		}

		protected override void DrawWindowContents(int windowId)
		{
			GUILayout.BeginVertical();

			DrawNameValueLine ("Target Altitude", flightControl.TargetAltitude.ToString ());
			DrawNameValueLine ("Pitch (Target)", flightControl.TargetPitch.ToString ());
			DrawNameValueLine ("Velocity (Horizontal)", flightControl.VHorizontal.ToString ());
			DrawNameValueLine ("Velocity (Vertical)", flightControl.VVertical.ToString ());
			DrawNameValueLine ("Best Angle", flightControl.BestAngle.ToString ());

			GUILayout.EndVertical();
		}

		private void DrawNameValueLine(String label, String value) 
		{
			GUILayout.BeginHorizontal(GUILayout.Width(FixedWidth));

			GUILayout.Label (label, NameStyle);
			GUILayout.FlexibleSpace ();
			GUILayout.TextArea (value, ValueStyle);

			GUILayout.EndHorizontal (); 
		}

		private void InitialiseStyles()
		{
			this.NameStyle = new GUIStyle (HighLogic.Skin.label) {
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

			this.ValueStyle = new GUIStyle (HighLogic.Skin.label) {
				margin = new RectOffset (),
				padding = new RectOffset (0, 5, 0, 0),
				alignment = TextAnchor.MiddleRight,
				fontSize = (int)(11),
				fontStyle = FontStyle.Normal,
				fixedHeight = 20.0f 
			};
		}
	}
}

