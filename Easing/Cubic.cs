// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

namespace Depra.Tween.Easing
{
	public static class Cubic
	{
		public static readonly IEasing EASE_IN;
		public static readonly IEasing EASE_OUT;
		public static readonly IEasing EASE_IN_OUT;
		public static readonly IEasing EASE_OUT_IN;

		static Cubic()
		{
			EASE_IN = new CubicEaseIn();
			EASE_OUT = new CubicEaseOut();
			EASE_IN_OUT = new CubicEaseInOut();
			EASE_OUT_IN = new CubicEaseOutIn();
		}
	}

	public readonly struct CubicEaseIn : IEasing
	{
		public float Calculate(float t, float b, float c, float d) => c * (t /= d) * t * t + b;
	}

	public readonly struct CubicEaseInOut : IEasing
	{
		public float Calculate(float t, float b, float c, float d) =>
			(t /= d / 2f) < 1f ? c / 2f * t * t * t + b : c / 2f * ((t -= 2f) * t * t + 2f) + b;
	}

	public readonly struct CubicEaseOut : IEasing
	{
		public float Calculate(float t, float b, float c, float d) => c * ((t = t / d - 1f) * t * t + 1f) + b;
	}

	public readonly struct CubicEaseOutIn : IEasing
	{
		public float Calculate(float t, float b, float c, float d) => t < d / 2f
			? c / 2f * ((t = t * 2f / d - 1f) * t * t + 1f) + b
			: c / 2f * (t = (t * 2f - d) / d) * t * t + b + c / 2f;
	}
}