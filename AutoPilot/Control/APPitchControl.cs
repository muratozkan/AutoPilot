using System;
using AutoPilot.Control;
using UnityEngine;

namespace AutoPilot
{
	public class APPitchControl : APControl
	{
		public const double PITCH_MAX = 0.3;

		// kP = 1 / 2 * deltaVMax
		private APPid pid = new APPid (0.25, 0.001, 0.001);

		#region implemented abstract members of APControl

		protected override APCommand Compute ()
		{
			double dAlt = Target.altitude - FlightData.altitude;

			double error = (targetPitch - (double) FlightData.rotation.Pitch ());
			double pitch = pid.Compute (error, TimeWarp.deltaTime);

			Debug.Log(string.Format ("AutoPilot: pTarget: {0} pitch: {1} error: {2}", targetPitch, pitch, error)); 

			return new APCommand () {
				pitch = (float) APUtils.Clamp(pitch, -PITCH_MAX, PITCH_MAX)
			};
		}

		#endregion
	}
}

