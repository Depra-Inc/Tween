// SPDX-License-Identifier: Apache-2.0
// © 2024-2025 Nikolay Melnikov <n.melnikov@depra.org>

namespace Depra.Easing
{
	public static class Quart
	{
		public static readonly IEase IN;
		public static readonly IEase OUT;
		public static readonly IEase IN_OUT;
		public static readonly IEase OUT_IN;

		static Quart()
		{
			IN = new QuarticEaseIn();
			OUT = new QuarticEaseOut();
			IN_OUT = new QuarticEaseInOut();
			OUT_IN = new QuarticEaseOutIn();
		}
	}

	public readonly struct QuarticEaseIn : IEase
	{
		public float Calculate(float t) => t * t * t * t;

		public float Calculate(float t, float b, float c, float d) => c * (t /= d) * t * t * t + b;
	}

	public readonly struct QuarticEaseOut : IEase
	{
		public float Calculate(float t) => 1.0f - (--t * t * t * t);

		public float Calculate(float t, float b, float c, float d) => -c * ((t = t / d - 1) * t * t * t - 1) + b;
	}

	public readonly struct QuarticEaseInOut : IEase
	{
		public float Calculate(float t) =>
			(t *= 2.0f) < 1.0f ? 0.5f * t * t * t * t : -0.5f * ((t -= 2.0f) * t * t * t - 2.0f);

		public float Calculate(float t, float b, float c, float d) => (t /= d / 2f) < 1f
			? c / 2f * t * t * t * t + b
			: -c / 2f * ((t -= 2f) * t * t * t - 2f) + b;
	}

	public readonly struct QuarticEaseOutIn : IEase
	{
		public float Calculate(float t) => throw new System.NotImplementedException();

		public float Calculate(float t, float b, float c, float d) => t < d / 2f
			? -(c / 2f) * ((t = t * 2f / d - 1f) * t * t * t - 1f) + b
			: c / 2f * (t = (t * 2f - d) / d) * t * t * t + (b + c / 2f);
	}
}