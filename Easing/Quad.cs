// SPDX-License-Identifier: Apache-2.0
// © 2024-2025 Nikolay Melnikov <n.melnikov@depra.org>

using System;

namespace Depra.Easing
{
	public static class Quad
	{
		public static readonly IEase IN;
		public static readonly IEase OUT;
		public static readonly IEase IN_OUT;
		public static readonly IEase OUT_IN;

		static Quad()
		{
			IN = new QuadraticEaseIn();
			OUT = new QuadraticEaseOut();
			IN_OUT = new QuadraticEaseInOut();
			OUT_IN = new QuadraticEaseOutIn();
		}
	}

	public readonly struct QuadraticEaseIn : IEase
	{
		public float Calculate(float t) => t * t;

		public float Calculate(float t, float b, float c, float d) => c * (t /= d) * t + b;
	}

	public readonly struct QuadraticEaseInOut : IEase
	{
		public float Calculate(float t)
		{
			t *= 2f;
			if (t < 1f)
			{
				return 0.5f * t * t;
			}

			t--;
			return -0.5f * (t * (t - 2f) - 1f);
		}

		public float Calculate(float t, float b, float c, float d) =>
			(t /= d / 2f) < 1f
				? c / 2f * t * t + b
				: -c / 2f * (--t * (t - 2f) - 1f) + b;
	}

	public readonly struct QuadraticEaseOut : IEase
	{
		public float Calculate(float t) => -t * (t - 2f);

		public float Calculate(float t, float b, float c, float d) => -c * (t /= d) * (t - 2f) + b;
	}

	public readonly struct QuadraticEaseOutIn : IEase
	{
		public float Calculate(float t) =>
			t < 0.5f ? 2.0f * t * t : 1.0f - MathF.Pow(-2.0f * t + 2.0f, 2.0f) / 2.0f;

		public float Calculate(float t, float b, float c, float d) => t < d / 2f
			? -(c / 2f) * (t = t * 2f / d) * (t - 2f) + b
			: c / 2f * (t = (t * 2f - d) / d) * t + (b + c / 2f);
	}
}