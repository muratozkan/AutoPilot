using System;

namespace AutoPilot
{
	public abstract class APControl
	{
		public APControlData ControlData { get; set; }

		public abstract APControl Update(APFlightData data, APTarget target);
	}
}
