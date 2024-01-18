// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

namespace Depra.Tween.Easing
{
	public static class Quart
	{
		public static readonly IEasing EASE_IN;
		public static readonly IEasing EASE_OUT;
		public static readonly IEasing EASE_IN_OUT;
		public static readonly IEasing EASE_OUT_IN;

		static Quart()
		{
			EASE_IN = new QuarticEaseIn();
			EASE_OUT = new QuarticEaseOut();
			EASE_IN_OUT = new QuarticEaseInOut();
			EASE_OUT_IN = new QuarticEaseOutIn();
		}
	}

	public readonly struct QuarticEaseIn : IEasing
	{
		public float Calculate(float t, float b, float c, float d) => c * (t /= d) * t * t * t + b;
	}

	public readonly struct QuarticEaseInOut : IEasing
	{
		public float Calculate(float t, float b, float c, float d) => (t /= d / 2f) < 1f
			? c / 2f * t * t * t * t + b
			: -c / 2f * ((t -= 2f) * t * t * t - 2f) + b;
	}

	public readonly struct QuarticEaseOut : IEasing
	{
		public float Calculate(float t, float b, float c, float d) => -c * ((t = t / d - 1) * t * t * t - 1) + b;
	}

	public readonly struct QuarticEaseOutIn : IEasing
	{
		public float Calculate(float t, float b, float c, float d) => t < d / 2f
			? -(c / 2f) * ((t = t * 2f / d - 1f) * t * t * t - 1f) + b
			: c / 2f * (t = (t * 2f - d) / d) * t * t * t + (b + c / 2f);
	}
}