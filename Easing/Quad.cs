// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

namespace Depra.Tween.Easing
{
	public static class Quad
	{
		public static readonly IEasing EASE_IN;
		public static readonly IEasing EASE_OUT;
		public static readonly IEasing EASE_IN_OUT;
		public static readonly IEasing EASE_OUT_IN;

		static Quad()
		{
			EASE_IN = new QuadraticEaseIn();
			EASE_OUT = new QuadraticEaseOut();
			EASE_IN_OUT = new QuadraticEaseInOut();
			EASE_OUT_IN = new QuadraticEaseOutIn();
		}
	}

	public readonly struct QuadraticEaseIn : IEasing
	{
		public float Calculate(float t, float b, float c, float d) => c * (t /= d) * t + b;
	}

	public readonly struct QuadraticEaseInOut : IEasing
	{
		public float Calculate(float t, float b, float c, float d) =>
			(t /= d / 2f) < 1f
				? c / 2f * t * t + b
				: -c / 2f * (--t * (t - 2f) - 1f) + b;
	}

	public readonly struct QuadraticEaseOut : IEasing
	{
		public float Calculate(float t, float b, float c, float d) => -c * (t /= d) * (t - 2f) + b;
	}

	public readonly struct QuadraticEaseOutIn : IEasing
	{
		public float Calculate(float t, float b, float c, float d) => t < d / 2f
			? -(c / 2f) * (t = t * 2f / d) * (t - 2f) + b
			: c / 2f * (t = (t * 2f - d) / d) * t + (b + c / 2f);
	}
}