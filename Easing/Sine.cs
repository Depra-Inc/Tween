// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

using System;

namespace Depra.Tween.Easing
{
	public static class Sine
	{
		public static readonly IEasing EASE_IN;
		public static readonly IEasing EASE_OUT;
		public static readonly IEasing EASE_IN_OUT;
		public static readonly IEasing EASE_OUT_IN;

		static Sine()
		{
			EASE_IN = new SineEaseIn();
			EASE_OUT = new SineEaseOut();
			EASE_IN_OUT = new SineEaseInOut();
			EASE_OUT_IN = new SineEaseOutIn();
		}
	}

	public readonly struct SineEaseIn : IEasing
	{
		public float Calculate(float t, float b, float c, float d) =>
			-c * (float) Math.Cos(t / d * ((float) Math.PI / 2f)) + c + b;
	}

	public readonly struct SineEaseInOut : IEasing
	{
		public float Calculate(float t, float b, float c, float d) =>
			-c / 2f * ((float) Math.Cos((float) Math.PI * t / d) - 1f) + b;
	}

	public readonly struct SineEaseOut : IEasing
	{
		public float Calculate(float t, float b, float c, float d) =>
			c * (float) Math.Sin(t / d * ((float) Math.PI / 2f)) + b;
	}

	public readonly struct SineEaseOutIn : IEasing
	{
		public float Calculate(float t, float b, float c, float d) => t < d / 2f
			? c / 2f * (float) Math.Sin(t * 2f / d * ((float) Math.PI / 2f)) + b
			: -(c / 2f) * (float) Math.Cos((t * 2f - d) / d * ((float) Math.PI / 2f)) + c / 2f + (b + c / 2f);
	}
}