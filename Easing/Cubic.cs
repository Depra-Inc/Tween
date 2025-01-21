// SPDX-License-Identifier: Apache-2.0
// © 2024-2025 Nikolay Melnikov <n.melnikov@depra.org>

namespace Depra.Easing
{
	public static class Cubic
	{
		public static readonly IEase IN;
		public static readonly IEase OUT;
		public static readonly IEase IN_OUT;
		public static readonly IEase OUT_IN;

		static Cubic()
		{
			IN = new CubicEaseIn();
			OUT = new CubicEaseOut();
			IN_OUT = new CubicEaseInOut();
			OUT_IN = new CubicEaseOutIn();
		}
	}

	public readonly struct CubicEaseIn : IEase
	{
		public float Calculate(float t) => t * t * t;

		public float Calculate(float t, float b, float c, float d) => c * (t /= d) * t * t + b;
	}

	public readonly struct CubicEaseOut : IEase
	{
		public float Calculate(float t)
		{
			t--;
			return t * t * t + 1f;
		}

		public float Calculate(float t, float b, float c, float d) => c * ((t = t / d - 1f) * t * t + 1f) + b;
	}

	public readonly struct CubicEaseInOut : IEase
	{
		public float Calculate(float t)
		{
			t *= 2f;
			if (t < 1f)
			{
				return 0.5f * t * t * t;
			}

			t -= 2f;
			return 0.5f * (t * t * t + 2f);
		}

		public float Calculate(float t, float b, float c, float d) =>
			(t /= d / 2f) < 1f ? c / 2f * t * t * t + b : c / 2f * ((t -= 2f) * t * t + 2f) + b;
	}

	public readonly struct CubicEaseOutIn : IEase
	{
		public float Calculate(float t) => throw new System.NotImplementedException();

		public float Calculate(float t, float b, float c, float d) => t < d / 2f
			? c / 2f * ((t = t * 2f / d - 1f) * t * t + 1f) + b
			: c / 2f * (t = (t * 2f - d) / d) * t * t + b + c / 2f;
	}
}