using System;

namespace AutoPilot
{
	public class APPid
	{
		private double prevError, integral;

		public double kP, kI, kD;
		public double max, min;

		public APPid(double kP, double kI, double kD, double min = double.MinValue, double max = double.MinValue) {
			this.kP = kP;
			this.kI = kI;
			this.kD = kD;
			this.min = min;
			this.max = max;
		}

		public double Compute(double error, double deltaT) {
			integral += (error * deltaT);
			integral = Math.Max (5, integral);	// limit the integral before it gets out of control
			double action = (kP * error) + (kI * integral) + (kD * (error - prevError) / deltaT);

			prevError = error;

			return action;
		}

		public void Reset() {
			integral = 0;
			prevError = 0;
		}
	}
}

