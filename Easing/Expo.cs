// SPDX-License-Identifier: Apache-2.0
// © 2024-2025 Nikolay Melnikov <n.melnikov@depra.org>

using System;

namespace Depra.Easing
{
	public static class Expo
	{
		public static readonly IEase IN;
		public static readonly IEase OUT;
		public static readonly IEase IN_OUT;
		public static readonly IEase OUT_IN;

		static Expo()
		{
			IN = new ExponentialEaseIn();
			OUT = new ExponentialEaseOut();
			IN_OUT = new ExponentialEaseInOut();
			OUT_IN = new ExponentialEaseOutIn();
		}
	}

	public readonly struct ExponentialEaseIn : IEase
	{
		public float Calculate(float t) =>
			t == 0f ? 0f : MathF.Pow(2f, 10f * t - 10f);

		public float Calculate(float t, float b, float c, float d) =>
			t == 0f ? b : c * MathF.Pow(2f, 10f * (t / d - 1f)) + b;
	}

	public readonly struct ExponentialEaseInOut : IEase
	{
		public float Calculate(float t) =>
			t == 1f ? 1f : 1f - MathF.Pow(2f, -10f * t);

		public float Calculate(float t, float b, float c, float d) => t == 0 ? b :
			t == d ? b + c :
			(t /= d / 2f) < 1f ? c / 2f * MathF.Pow(2f, 10f * (t - 1f)) + b :
			c / 2f * (2f - MathF.Pow(2f, -10f * --t)) + b;
	}

	public readonly struct ExponentialEaseOut : IEase
	{
		public float Calculate(float t) => t == 0f ? 0f
			: t == 1f ? 1f
			: t < 0.5f ? MathF.Pow(2f, 20f * t - 10f) / 2f
			: (2f - MathF.Pow(2f, -20f * t + 10f)) / 2f;

		public float Calculate(float t, float b, float c, float d) =>
			t == d ? b + c : c * (1f - MathF.Pow(2f, -10f * t / d)) + b;
	}

	public readonly struct ExponentialEaseOutIn : IEase
	{
		public float Calculate(float t) => throw new NotImplementedException();

		public float Calculate(float t, float b, float c, float d) => t < d / 2f
			? t * 2f == d ? b + c / 2f : c / 2f * (1 - MathF.Pow(2f, -10f * t * 2f / d)) + b
			: t * 2f - d == 0f
				? b + c / 2f
				: c / 2f * MathF.Pow(2f, 10f * ((t * 2f - d) / d - 1f)) + b + c / 2f;
	}
}