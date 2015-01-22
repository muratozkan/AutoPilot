using System;
using UnityEngine;
using AutoPilot.Control;

namespace AutoPilot.UI
{
	public class APStatusWindow : WindowBase
	{
		private APControl flightControl;

		public GUIStyle NameStyle { get; set; }

		public GUIStyle ValueStyle { get; set; }

		private const float FixedWidth = 300f;

		public APStatusWindow (APControl flightControl) : base("AP Status")
		{
			// Force default size
			this.flightControl = flightControl;

			windowPos = new Rect(60, 60, 230, 100);

			InitialiseStyles ();
		}

		protected override void DrawWindowContents(int windowId)
		{
			GUILayout.BeginVertical();

			DrawNameValueLine ("Target Altitude", flightControl.Target.altitude.ToString ());
			DrawNameValueLine ("Current Altitude", flightControl.FlightData.altitude.ToString ());

			DrawNameValueLine ("Pitch (Target)", flightControl.Command.pitch.ToString ());
			DrawNameValueLine ("Velocity (Horizontal)", flightControl.FlightData.vHorizontal.ToString ());
			DrawNameValueLine ("Velocity (Vertical)", flightControl.FlightData.vVertical.ToString ());

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

