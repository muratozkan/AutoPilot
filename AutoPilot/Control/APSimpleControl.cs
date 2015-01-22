using System;
using UnityEngine;

namespace AutoPilot.Control
{
	public class APSimpleControl : APControl
	{
		#region implemented abstract members of APControl

		protected override APCommand Compute ()
		{
			float deltaX = (float) (Target.altitude - FlightData.altitude);

			float tanAlpha = deltaX / (FlightData.vHorizontal * 8);
			float target = Mathf.Atan (tanAlpha) * 2 / Mathf.PI;

			int signAdjust = (deltaX > 0f && (FlightData.vVertical * 16) > deltaX) ? -1 : 1;

			Debug.Log(string.Format ("AutoPilot: deltaX: {0} target * sa: {1}", deltaX, target * signAdjust)); 

			return new APCommand {
				pitch = Mathf.MoveTowards (FlightData.rotation.Pitch (), target * signAdjust, 0.1f)
			};
		}
		#endregion
	}
}

