using System;
using UnityEngine;

namespace AutoPilot.Control
{
	public class APSimpleControl : APControl
	{
		#region implemented abstract members of APControl

		public override void Update (float time)
		{
			float altitudeDiff = (float) (Target.altitude - FlightData.altitude);

			float tToAlt = APUtils.AlmostEqual(FlightData.vVertical, 0f, 1E-03f) ? float.MaxValue : Mathf.Abs(altitudeDiff) / Mathf.Abs(FlightData.vVertical);

			float pitch = (tToAlt > 5) ? 0.3f : tToAlt * (1f / 5) * 0.1f; 

			if (altitudeDiff < 0)
				pitch = -pitch;

			int isPos = Command.pitch > 0 ? 1 : -1;
			if (Math.Abs (FlightData.rotation.Pitch ()) >= Math.Abs (Command.pitch))
				pitch = -pitch;
			 
			Command = new APCommand {
				pitch = pitch
			};
		}

		#endregion
	}
}

