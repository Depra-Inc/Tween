// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

namespace Depra.Tween.Easing
{
	public static class Bounce
	{
		public static readonly IEasing EASE_IN;
		public static readonly IEasing EASE_OUT;
		public static readonly IEasing EASE_IN_OUT;
		public static readonly IEasing EASE_OUT_IN;

		static Bounce()
		{
			EASE_IN =  new BounceEaseIn();
			EASE_OUT = new BounceEaseOut();
			EASE_IN_OUT = new BounceEaseInOut();
			EASE_OUT_IN = new BounceEaseOutIn();
		}
	}

	public readonly struct BounceEaseIn : IEasing
	{
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

	public readonly struct BounceEaseInOut : IEasing
	{
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

	public readonly struct BounceEaseOut : IEasing
	{
		public float Calculate(float t, float b, float c, float d) =>
			(t /= d) < (1f / 2.75f)
				? c * (7.5625f * t * t) + b
				: t switch
				{
					< 2f / 2.75f => c * (7.5625f * (t -= 1.5f / 2.75f) * t + 0.75f) + b,
					< 2.5f / 2.75f => c * (7.5625f * (t -= 2.25f / 2.75f) * t + 0.9375f) + b,
					_ => c * (7.5625f * (t -= 2.625f / 2.75f) * t + 0.984375f) + b
				};
	}

	public readonly struct BounceEaseOutIn : IEasing
	{
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