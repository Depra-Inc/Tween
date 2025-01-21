// SPDX-License-Identifier: Apache-2.0
// © 2024-2025 Nikolay Melnikov <n.melnikov@depra.org>

namespace Depra.Easing
{
	public static class Bounce
	{
		public static readonly IEase IN;
		public static readonly IEase OUT;
		public static readonly IEase IN_OUT;
		public static readonly IEase OUT_IN;

		static Bounce()
		{
			IN = new BounceEaseIn();
			OUT = new BounceEaseOut();
			IN_OUT = new BounceEaseInOut();
			OUT_IN = new BounceEaseOutIn();
		}
	}

	public readonly struct BounceEaseIn : IEase
	{
		public float Calculate(float t) => 1.0f - Bounce.OUT.Calculate(1.0f - t);

		public float Calculate(float t, float b, float c, float d) =>
			(t = (d - t) / d) < 1f / 2.75f
				? c - c * (7.5625f * t * t) + b
				: t switch
				{
					< 2f / 2.75f => c - c * (7.5625f * (t -= 1.5f / 2.75f) * t + 0.75f) + b,
					< 2.5f / 2.75f => c - c * (7.5625f * (t -= 2.25f / 2.75f) * t + 0.9375f) + b,
					_ => c - c * (7.5625f * (t -= (2.625f / 2.75f)) * t + 0.984375f) + b
				};
	}

	public readonly struct BounceEaseOut : IEase
	{
		public float Calculate(float t) => t switch
		{
			< 1.0f / 2.75f => 7.5625f * t * t,
			< 2.0f / 2.75f => 7.5625f * (t -= 1.5f / 2.75f) * t + 0.75f,
			< 2.5f / 2.75f => 7.5625f * (t -= 2.25f / 2.75f) * t + 0.9375f,
			_ => 7.5625f * (t -= 2.625f / 2.75f) * t + 0.984375f
		};

		public float Calculate(float t, float b, float c, float d) =>
			(t /= d) < (1f / 2.75f)
				? c * (7.5625f * t * t) + b
				: t switch
				{
					< 2.0f / 2.75f => c * (7.5625f * (t -= 1.5f / 2.75f) * t + 0.75f) + b,
					< 2.5f / 2.75f => c * (7.5625f * (t -= 2.25f / 2.75f) * t + 0.9375f) + b,
					_ => c * (7.5625f * (t -= 2.625f / 2.75f) * t + 0.984375f) + b
				};
	}

	public readonly struct BounceEaseInOut : IEase
	{
		public float Calculate(float t) => t < 0.5f
			? Bounce.IN.Calculate(t * 2f) * 0.5f
			: Bounce.OUT.Calculate(t * 2f - 1f) * 0.5f + 0.5f;

		public float Calculate(float t, float b, float c, float d) => t < d / 2f
			? (t = (d - t * 2f) / d) < (1f / 2.75f)
				? (c - c * (7.5625f * t * t)) * 0.5f + b
				: t switch
				{
					< 2f / 2.75f => (c - c * (7.5625f * (t -= (1.5f / 2.75f)) * t + 0.75f)) * 0.5f + b,
					< 2.5f / 2.75f => (c - c * (7.5625f * (t -= 2.25f / 2.75f) * t + 0.9375f)) * 0.5f + b,
					_ => (c - c * (7.5625f * (t -= 2.625f / 2.75f) * t + 0.984375f)) * 0.5f + b
				}
			: (t = (t * 2f - d) / d) < 1f / 2.75f
				? c * (7.5625f * t * t) * 0.5f + c * 0.5f + b
				: t switch
				{
					< 2f / 2.75f => (c * (7.5625f * (t -= 1.5f / 2.75f) * t + 0.75f)) * 0.5f + c * 0.5f + b,
					< 2.5f / 2.75f => c * (7.5625f * (t -= 2.25f / 2.75f) * t + 0.9375f) * 0.5f + c * 0.5f + b,
					_ => c * (7.5625f * (t -= 2.625f / 2.75f) * t + 0.984375f) * 0.5f + c * 0.5f + b
				};
	}

	public readonly struct BounceEaseOutIn : IEase
	{
		public float Calculate(float t) => throw new System.NotImplementedException();

		public float Calculate(float t, float b, float c, float d) => t < d / 2f == false
			? (t = (d - (t * 2f - d)) / d) < 1f / 2.75f
				? c / 2f - c / 2f * (7.5625f * t * t) + (b + c / 2)
				: t switch
				{
					< 2f / 2.75f => c / 2f - c / 2f * (7.5625f * (t -= 1.5f / 2.75f) * t + 0.75f) + (b + c / 2f),
					< 2.5f / 2.75f => c / 2f - c / 2f * (7.5625f * (t -= 2.25f / 2.75f) * t + 0.9375f) + (b + c / 2f),
					_ => c / 2f - c / 2f * (7.5625f * (t -= 2.625f / 2.75f) * t + 0.984375f) + (b + c / 2f)
				}
			: (t = (t * 2f) / d) < (1f / 2.75f)
				? c / 2f * (7.5625f * t * t) + b
				: t switch
				{
					< 2f / 2.75f => c / 2f * (7.5625f * (t -= 1.5f / 2.75f) * t + 0.75f) + b,
					< 2.5f / 2.75f => c / 2f * (7.5625f * (t -= 2.25f / 2.75f) * t + 0.9375f) + b,
					_ => c / 2f * (7.5625f * (t -= 2.625f / 2.75f) * t + 0.984375f) + b
				};
	}
}