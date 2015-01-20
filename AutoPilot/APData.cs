﻿using System;
using UnityEngine;

namespace AutoPilot
{
	/// <summary>
	/// Autopilot controller parameters
	/// </summary>
	public struct APParams 
	{
		public float kP;
		public float kI;
		public float kD;
	}

	/// <summary>
	/// Sensor data to be filled into the controller
	/// </summary>
	public struct APFlightData
	{
		public Vector3 velocity;
		public Quaternion rotation;

		public float vVertical;
		public float vHorizontal;
		public float altitude;
	}

	/// <summary>
	/// Controller target condition
	/// </summary>
	public struct APTarget 
	{
		public float altitude;
	}

	/// <summary>
	/// The actuator commands that are generated by controller
	/// </summary>
	public struct APCommand
	{
		public float pitch;
	}
}
