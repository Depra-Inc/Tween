// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

namespace Depra.Tween.Easing
{
	public static class Quint
	{
		public static readonly IEasing EASE_IN;
		public static readonly IEasing EASE_OUT;
		public static readonly IEasing EASE_IN_OUT;
		public static readonly IEasing EASE_OUT_IN;

		static Quint()
		{
			EASE_IN = new QuinticEaseIn();
			EASE_OUT = new QuinticEaseOut();
			EASE_IN_OUT = new QuinticEaseInOut();
			EASE_OUT_IN = new QuinticEaseOutIn();
		}
	}

	public readonly struct QuinticEaseIn : IEasing
	{
		public float Calculate(float t, float b, float c, float d) => c * (t /= d) * t * t * t * t + b;
	}

	public readonly struct QuinticEaseInOut : IEasing
	{
		public float Calculate(float t, float b, float c, float d) => (t /= d / 2) < 1
			? c / 2 * t * t * t * t * t + b
			: c / 2 * ((t -= 2) * t * t * t * t + 2) + b;
	}

	public readonly struct QuinticEaseOut : IEasing
	{
		public float Calculate(float t, float b, float c, float d) => c * ((t = t / d - 1) * t * t * t * t + 1) + b;
	}

	public readonly struct QuinticEaseOutIn : IEasing
	{
		public float Calculate(float t, float b, float c, float d) => t < d / 2
			? c / 2 * ((t = t * 2 / d - 1) * t * t * t * t + 1) + b
			: c / 2 * (t = (t * 2 - d) / d) * t * t * t * t + (b + c / 2);
	}
}