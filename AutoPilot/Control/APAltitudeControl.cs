using System;
using AutoPilot.Control;
using UnityEngine;

namespace AutoPilot
{
	public class APAltitudeControl : APControl
	{
		private float error;
		private float prevError;

		private float integral = 0.0f;
		private float derivative = 0.0f;

		#region implemented abstract members of APControl

		protected override APCommand Update ()
		{
			error = Target.altitude - FlightData.altitude;

			integral = integral + (error * Time.deltaTime);
			derivative = (error - prevError) / Time.deltaTime;

			float pitch = (Params.kP * error) + (Params.kI * integral) + (Params.kD * derivative);

			prevError = error;

			Debug.Log(string.Format ("AutoPilot: pitch: {0} error: {1}", pitch, error)); 

			return new APCommand () {
				pitch = Mathf.Clamp (pitch, 1f, -1f)
			};
		}

		#endregion
	}
}

