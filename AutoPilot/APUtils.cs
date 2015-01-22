using System;
using UnityEngine;

namespace AutoPilot
{
	public class APUtils
	{
		public static Rect EnsureVisible(Rect pos, float min = 16.0f)
		{
			float xMin = min - pos.width;
			float xMax = Screen.width - min;
			float yMin = min - pos.height;
			float yMax = Screen.height - min;

			pos.x = Mathf.Clamp(pos.x, xMin, xMax);
			pos.y = Mathf.Clamp(pos.y, yMin, yMax);

			return pos;
		}

		public static bool IsPaused() 
		{
			bool paused = false;
			if (HighLogic.LoadedSceneIsFlight)
			{
				try
				{
					paused = PauseMenu.isOpen || FlightResultsDialog.isDisplaying;
				}
				catch (Exception)
				{
					// ignore the error and assume the pause menu is not open
				}
			}
			return paused;
		}

		public static bool AlmostEqual(float x, float y, float eps = 1E-15f)
		{
			float epsilon = Math.Max(Math.Abs(x), Math.Abs(y)) * eps;
			return Math.Abs(x - y) <= epsilon;
		}

		/// <summary>
		/// Clamp the specified value between min and max. Assumes min < max
		/// </summary>
		/// <param name="x">Value to be clamped.</param>
		/// <param name="min">Minimum.</param>
		/// <param name="max">Max.</param>
		public static double Clamp(double x, double min, double max) {
			return Math.Min (max, Math.Max (x, min));
		}
	}
}

