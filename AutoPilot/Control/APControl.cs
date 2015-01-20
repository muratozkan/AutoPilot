using System;

namespace AutoPilot.Control
{
	public abstract class APControl
	{
		public APParams Params { get; set; }

		public APFlightData FlightData { get; private set; }

		public APTarget Target { get; set; }

		public APCommand Command { get; private set; }

		/// <summary>
		/// Calculates the command to be applied to the actuators using the target and flight data and sets the result in "Command" field.
		/// </summary>
		/// <param name="time">Time.</param>
		protected abstract APCommand Update();

		public void Update(APFlightData flightData) {
			FlightData = flightData;
			Command = Update ();
		}
	}
}
