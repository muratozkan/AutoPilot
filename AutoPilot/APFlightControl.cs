using System;
using UnityEngine;

namespace AutoPilot
{
	public class APFlightControl 
	{

		public float TargetAltitude { get; set; }

		public float BestAngle { get; private set; }

		public float VVertical { get; private set; }

		public float VHorizontal { get; private set; }

		public float TargetPitch { get; private set; }

		public APFlightControl () {
			// Set the target altitude manually
			TargetAltitude = 1000f;
		}

		// This doesn't work for some reason
		public void OnFlightReady (Vessel vessel)
		{
			Debug.Log ("AutoPilot: OnFlightReady");
		}

		public void OnFlyByWire(FlightCtrlState state) 
		{
			Vessel vessel = FlightGlobals.ActiveVessel;

			float altitudeDiff = (float) (TargetAltitude - vessel.altitude);

			VVertical = (float) vessel.verticalSpeed;
			VHorizontal = (float) vessel.horizontalSrfSpeed;

			float tToAlt = APUtils.AlmostEqual(VVertical, 0f, 1E-03f) ? float.MaxValue : Mathf.Abs(altitudeDiff) / Mathf.Abs(VVertical);

			TargetPitch = (tToAlt > 5) ? 0.3f : tToAlt * (1f / 5) * 0.1f; 

			if (altitudeDiff < 0)
				TargetPitch = -TargetPitch;

			int isPos = TargetPitch > 0 ? 1 : -1;
			if (Math.Abs(vessel.srfRelRotation.Pitch ()) < Math.Abs(TargetPitch))
				state.pitch += (0.1f * isPos);
			else
				state.pitch -= (0.1f * isPos);
		}
	}
}