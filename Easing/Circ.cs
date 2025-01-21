// SPDX-License-Identifier: Apache-2.0
// © 2024-2025 Nikolay Melnikov <n.melnikov@depra.org>

using System;

namespace Depra.Easing
{
	public static class Circ
	{
		public static readonly IEase IN;
		public static readonly IEase OUT;
		public static readonly IEase IN_OUT;
		public static readonly IEase OUT_IN;

		static Circ()
		{
			IN = new CircularEaseIn();
			OUT = new CircularEaseOut();
			IN_OUT = new CircularEaseInOut();
			OUT_IN = new CircularEaseOutIn();
		}
	}

	public readonly struct CircularEaseIn : IEase
	{
		public float Calculate(float t) => 1f - MathF.Sqrt(1f - MathF.Pow(t, 2f));

		public float Calculate(float t, float b, float c, float d) => -c * (MathF.Sqrt(1f - (t /= d) * t) - 1f) + b;
	}

	public readonly struct CircularEaseOut : IEase
	{
		public float Calculate(float t) => MathF.Sqrt(1f - MathF.Pow(t - 1f, 2f));

		public float Calculate(float t, float b, float c, float d) => c * MathF.Sqrt(1f - (t = t / d - 1f) * t) + b;
	}

	public readonly struct CircularEaseInOut : IEase
	{
		public float Calculate(float t) => t < 0.5f
			? (1.0f - MathF.Sqrt(1.0f - MathF.Pow(2.0f * t, 2.0f))) / 2.0f
			: (MathF.Sqrt(1.0f - MathF.Pow(-2.0f * t + 2.0f, 2.0f)) + 1.0f) / 2.0f;

		public float Calculate(float t, float b, float c, float d) => (t /= d / 2f) < 1f
			? -c / 2f * (MathF.Sqrt(1f - t * t) - 1f) + b
			: c / 2f * (MathF.Sqrt(1f - (t -= 2f) * t) + 1f) + b;
	}

	public readonly struct CircularEaseOutIn : IEase
	{
		public float Calculate(float t) => throw new NotImplementedException();

		public float Calculate(float t, float b, float c, float d) => t < d / 2f
			? (c / 2f) * MathF.Sqrt(1f - (t = (t * 2f) / d - 1f) * t) + b
			: -(c / 2f) * (MathF.Sqrt(1f - (t = (t * 2f - d) / d) * t) - 1f) + (b + c / 2f);
	}
}