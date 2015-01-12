using System;

namespace AutoPilot.Control
{
	public abstract class APControl
	{
		public APParams Params { get; set; }

		public APFlightData FlightData { get; set; }

		public APTarget Target { get; set; }

		public APCommand Command { get; protected set; }

		/// <summary>
		/// Calculates the command to be applied to the actuators using the target and flight data and sets the result in "Command" field.
		/// </summary>
		/// <param name="time">Time.</param>
		public abstract void Update(float time);
	}
}
