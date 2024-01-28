// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

using System;

namespace Depra.Easing
{
	public static class Expo
	{
		public static readonly IEasing EASE_IN;
		public static readonly IEasing EASE_OUT;
		public static readonly IEasing EASE_IN_OUT;
		public static readonly IEasing EASE_OUT_IN;

		static Expo()
		{
			EASE_IN = new ExponentialEaseIn();
			EASE_OUT = new ExponentialEaseOut();
			EASE_IN_OUT = new ExponentialEaseInOut();
			EASE_OUT_IN = new ExponentialEaseOutIn();
		}
	}

	public readonly struct ExponentialEaseIn : IEasing
	{
		public float Calculate(float t, float b, float c, float d) =>
			t == 0f ? b : c * (float) Math.Pow(2f, 10f * (t / d - 1f)) + b;
	}

	public readonly struct ExponentialEaseInOut : IEasing
	{
		public float Calculate(float t, float b, float c, float d) =>
			t == 0
				? b
				: t == d
					? b + c
					: (t /= d / 2.0f) < 1.0f
						? c / 2f * (float) Math.Pow(2f, 10f * (t - 1f)) + b
						: c / 2f * (2f - (float) Math.Pow(2f, -10f * --t)) + b;
	}

	public readonly struct ExponentialEaseOut : IEasing
	{
		public float Calculate(float t, float b, float c, float d) =>
			t == d ? b + c : c * (1f - (float) Math.Pow(2f, -10f * t / d)) + b;
	}

	public readonly struct ExponentialEaseOutIn : IEasing
	{
		public float Calculate(float t, float b, float c, float d) =>
			t < d / 2.0f
				? t * 2.0f == d ? b + c / 2.0f : c / 2.0f * (1 - (float) Math.Pow(2f, -10f * t * 2.0f / d)) + b
				: t * 2.0f - d == 0f
					? b + c / 2.0f
					: c / 2.0f * (float) Math.Pow(2f, 10f * ((t * 2f - d) / d - 1f)) + b + c / 2.0f;
	}
}