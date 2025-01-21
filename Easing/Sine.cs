// SPDX-License-Identifier: Apache-2.0
// © 2024-2025 Nikolay Melnikov <n.melnikov@depra.org>

using System;

namespace Depra.Easing
{
	public static class Sine
	{
		public static readonly IEase IN;
		public static readonly IEase OUT;
		public static readonly IEase IN_OUT;
		public static readonly IEase OUT_IN;

		static Sine()
		{
			IN = new SineEaseIn();
			OUT = new SineEaseOut();
			IN_OUT = new SineEaseInOut();
			OUT_IN = new SineEaseOutIn();
		}
	}

	public readonly struct SineEaseIn : IEase
	{
		public float Calculate(float t) => 1f - MathF.Cos(t * (MathF.PI * 0.5f));

		public float Calculate(float t, float b, float c, float d) =>
			-c * MathF.Cos(t / d * ((float)Math.PI * 0.5f)) + c + b;
	}

	public readonly struct SineEaseOut : IEase
	{
		public float Calculate(float t) => MathF.Sin(t * (MathF.PI * 0.5f));

		public float Calculate(float t, float b, float c, float d) =>
			c * MathF.Sin(t / d * ((float)Math.PI / 2f)) + b;
	}

	public readonly struct SineEaseInOut : IEase
	{
		public float Calculate(float t) => 0.5f * (1.0f - MathF.Cos(MathF.PI * t));

		public float Calculate(float t, float b, float c, float d) =>
			-c / 2f * ((float)Math.Cos((float)Math.PI * t / d) - 1f) + b;
	}

	public readonly struct SineEaseOutIn : IEase
	{
		public float Calculate(float t) => throw new NotImplementedException();

		public float Calculate(float t, float b, float c, float d) => t < d / 2f
			? c / 2f * MathF.Sin(t * 2f / d * (MathF.PI / 2f)) + b
			: -(c / 2f) * MathF.Cos((t * 2f - d) / d * (MathF.PI / 2f)) + c / 2f + (b + c / 2f);
	}
}