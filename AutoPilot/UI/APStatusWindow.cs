using System;
using UnityEngine;
using AutoPilot.Control;

namespace AutoPilot.UI
{
	public class APStatusWindow : WindowBase
	{
		private APControl flightControl;

		private const float FixedWidth = 300f;

		public APStatusWindow (APControl flightControl) : base("AP Status")
		{
			// Force default size
			this.flightControl = flightControl;

			windowPos = new Rect(60, 60, 230, 100);
		}

		protected override void DrawWindowContents(int windowId)
		{
			GUILayout.BeginVertical();

			DrawNameValueLine ("Target Altitude", flightControl.Target.altitude.ToString ());
			DrawNameValueLine ("Current Altitude", flightControl.FlightData.altitude.ToString ());
			DrawNameValueLine ("", "");
			DrawNameValueLine ("Pitch (Target)", flightControl.Command.pitch.ToString ());
			DrawNameValueLine ("Velocity (Horizontal)", flightControl.FlightData.vHorizontal.ToString ());
			DrawNameValueLine ("Velocity (Vertical)", flightControl.FlightData.vVertical.ToString ());
			DrawNameValueLine ("", "");
			DrawNameTextBox ("Altitude", "1000", (string t) => { Debug.Log (t); });

			GUILayout.EndVertical();
		}
	}
}

