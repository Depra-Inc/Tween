// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

using System;

namespace Depra.Tween.Easing
{
	public static class Circ
	{
		public static readonly IEasing EASE_IN;
		public static readonly IEasing EASE_OUT;
		public static readonly IEasing EASE_IN_OUT;
		public static readonly IEasing EASE_OUT_IN;

		static Circ()
		{
			EASE_IN = new CircularEaseIn();
			EASE_OUT = new CircularEaseOut();
			EASE_IN_OUT = new CircularEaseInOut();
			EASE_OUT_IN = new CircularEaseOutIn();
		}
	}

	public readonly struct CircularEaseIn : IEasing
	{
		public float Calculate(float t, float b, float c, float d) =>
			-c * ((float) Math.Sqrt(1f - (t /= d) * t) - 1f) + b;
	}

	public readonly struct CircularEaseInOut : IEasing
	{
		public float Calculate(float t, float b, float c, float d) => (t /= d / 2f) < 1f
			? -c / 2f * ((float) Math.Sqrt(1f - t * t) - 1f) + b
			: c / 2f * ((float) Math.Sqrt(1f - (t -= 2f) * t) + 1f) + b;
	}

	public readonly struct CircularEaseOut : IEasing
	{
		public float Calculate(float t, float b, float c, float d) =>
			c * (float) Math.Sqrt(1f - (t = t / d - 1f) * t) + b;
	}

	public readonly struct CircularEaseOutIn : IEasing
	{
		public float Calculate(float t, float b, float c, float d) => t < d / 2f
			? (c / 2f) * (float) Math.Sqrt(1f - (t = (t * 2f) / d - 1f) * t) + b
			: -(c / 2f) * ((float) Math.Sqrt(1f - (t = (t * 2f - d) / d) * t) - 1f) + (b + c / 2f);
	}
}