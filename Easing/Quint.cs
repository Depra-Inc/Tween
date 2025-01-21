// SPDX-License-Identifier: Apache-2.0
// © 2024-2025 Nikolay Melnikov <n.melnikov@depra.org>

namespace Depra.Easing
{
	public static class Quint
	{
		public static readonly IEase IN;
		public static readonly IEase OUT;
		public static readonly IEase IN_OUT;
		public static readonly IEase OUT_IN;

		static Quint()
		{
			IN = new QuinticEaseIn();
			OUT = new QuinticEaseOut();
			IN_OUT = new QuinticEaseInOut();
			OUT_IN = new QuinticEaseOutIn();
		}
	}

	public readonly struct QuinticEaseIn : IEase
	{
		public float Calculate(float t) => t * t * t * t * t;

		public float Calculate(float t, float b, float c, float d) => c * (t /= d) * t * t * t * t + b;
	}

	public readonly struct QuinticEaseOut : IEase
	{
		public float Calculate(float t) => --t * t * t * t * t + 1f;

		public float Calculate(float t, float b, float c, float d) => c * ((t = t / d - 1) * t * t * t * t + 1) + b;
	}

	public readonly struct QuinticEaseInOut : IEase
	{
		public float Calculate(float t)
		{
			t *= 2f;
			if (t < 1f)
			{
				var vv1 = t * t;
				return 0.5f * vv1 * vv1 * t;
			}

			t -= 2f;
			var vv2 = t * t;
			return 0.5f * (vv2 * vv2 * t + 2);
		}

		public float Calculate(float t, float b, float c, float d) => (t /= d / 2) < 1
			? c / 2 * t * t * t * t * t + b
			: c / 2 * ((t -= 2) * t * t * t * t + 2) + b;
	}

	public readonly struct QuinticEaseOutIn : IEase
	{
		public float Calculate(float t) => throw new System.NotImplementedException();

		public float Calculate(float t, float b, float c, float d) => t < d / 2
			? c / 2 * ((t = t * 2 / d - 1) * t * t * t * t + 1) + b
			: c / 2 * (t = (t * 2 - d) / d) * t * t * t * t + (b + c / 2);
	}
}