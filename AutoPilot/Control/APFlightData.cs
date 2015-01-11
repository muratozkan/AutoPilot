using System;
using UnityEngine;

namespace AutoPilot
{
	public struct APFlightData
	{
		public Vector3 velocity;
		public float altitude;
		public Quaternion rotation;

		public APFlightData(Vector3 velocity, float altitude, Quaternion rotation) {
			this.velocity = velocity;
			this.altitude = altitude;
			this.rotation = rotation;
		}
	}
}

