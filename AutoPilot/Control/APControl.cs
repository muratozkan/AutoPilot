using System;

namespace AutoPilot.Control
{
	public abstract class APControl
	{
		public bool IsEnabled { get; set; }

		public APParams Params { get; set; }

		public APFlightData FlightData { get; private set; }

		public APTarget Target { get; set; }

		public APCommand Command { get; private set; }

		public APControl() {
			IsEnabled = true;
		}

		/// <summary>
		/// Calculates the command to be applied to the actuators using the target and flight data and sets the result in "Command" field.
		/// </summary>
		/// <param name="time">Time.</param>
		protected abstract APCommand Compute();

		public void Update(APFlightData flightData) {
			FlightData = flightData;
			if (IsEnabled) {
				Command = Compute ();
			} 
		}
	}
}
