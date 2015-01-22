using System;
using AutoPilot.Control;
using UnityEngine;

namespace AutoPilot
{
	public class APAltitudeControl : APControl
	{
		public const double DELTA_V_MAX = 20;
		public const double PITCH_MAX = 0.3;

		// kP = 1 / 2 * deltaVMax
		private APPid pid = new APPid (0.025, 0.001, 0.01);

		#region implemented abstract members of APControl

		protected override APCommand Compute ()
		{
			double dAlt = Target.altitude - FlightData.altitude;
			double vTarget = dAlt / 5;		// assume steady state in 5 seconds

			vTarget = APUtils.Clamp (vTarget, -DELTA_V_MAX, DELTA_V_MAX);

			double error = (vTarget - FlightData.vVertical);
			double pitch = pid.Compute (error, TimeWarp.deltaTime);

			Debug.Log(string.Format ("AutoPilot: vTarget: {0} pitch: {1} error: {2}", vTarget, pitch, error)); 

			return new APCommand () {
				pitch = (float) APUtils.Clamp(pitch, -PITCH_MAX, PITCH_MAX)
			};
		}

		#endregion
	}
}

