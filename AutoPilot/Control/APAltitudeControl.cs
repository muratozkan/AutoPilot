using System;
using AutoPilot.Control;
using UnityEngine;

namespace AutoPilot
{
	public class APAltitudeControl : APControl
	{
		public const double deltaVMax = 20;

		// kP = 1 / 2 * deltaVMax
		private APPid pid = new APPid (0.025, 0.0001, 0.001);

		#region implemented abstract members of APControl

		protected override APCommand Compute ()
		{
			double dAlt = Target.altitude - FlightData.altitude;
			double vTarget = dAlt / 5;		// assume steady state in 5 seconds

			vTarget = Math.Min (deltaVMax, Math.Max (vTarget, -deltaVMax));

			// double vNormDenom = Math.Abs (FlightData.vVertical) < 1 ? 1 : Math.Abs (FlightData.vVertical);

			double error = (vTarget - FlightData.vVertical); // / vNormDenom;
			double pitch = pid.Compute (error, TimeWarp.deltaTime);

			Debug.Log(string.Format ("AutoPilot: vTarget: {0} pitch: {1} error: {2}", vTarget, pitch, error)); 

			return new APCommand () {
				pitch = (float) pitch
			};
		}

		#endregion
	}
}

