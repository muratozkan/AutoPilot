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

		public static bool AlmostEqual(float x, float y, float eps = 1E-15f)
		{
			float epsilon = Math.Max(Math.Abs(x), Math.Abs(y)) * eps;
			return Math.Abs(x - y) <= epsilon;
		}
	}
}

