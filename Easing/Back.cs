// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

namespace Depra.Tween.Easing
{
	public static class Back
	{
		public static readonly IEasing EASE_IN;
		public static readonly IEasing EASE_OUT;
		public static readonly IEasing EASE_IN_OUT;
		public static readonly IEasing EASE_OUT_IN;

		static Back()
		{
			EASE_IN = new BackEaseIn();
			EASE_OUT = new BackEaseOut();
			EASE_IN_OUT = new BackEaseInOut();
			EASE_OUT_IN = new BackEaseOutIn();
		}
	}

	public sealed record BackEaseIn : IEasing
	{
		private readonly float _s;

		public BackEaseIn(float s = 1.70158f) => _s = s;

		public float Calculate(float t, float b, float c, float d) => c * (t /= d) * t * ((_s + 1) * t - _s) + b;
	}

	public sealed record BackEaseInOut : IEasing
	{
		private readonly float _s;

		public BackEaseInOut(float s = 1.70158f) => _s = s;

		public float Calculate(float t, float b, float c, float d) => (t /= d / 2f) < 1f
			? c / 2f * (t * t * (((_s * 1.525f) + 1f) * t - _s * 1.525f)) + b
			: c / 2f * ((t -= 2f) * t * ((_s * 1.525f + 1f) * t + _s * 1.525f) + 2f) + b;
	}

	public sealed record BackEaseOut : IEasing
	{
		private readonly float _s;

		public BackEaseOut(float s = 1.70158f) => _s = s;

		public float Calculate(float t, float b, float c, float d) =>
			c * ((t = t / d - 1) * t * ((_s + 1) * t + _s) + 1) + b;
	}

	public sealed record BackEaseOutIn : IEasing
	{
		private readonly float _s;

		public BackEaseOutIn(float s = 1.70158f) => _s = s;

		public float Calculate(float t, float b, float c, float d) => t < d / 2
			? (c / 2) * ((t = (t * 2) / d - 1) * t * ((_s + 1) * t + _s) + 1) + b
			: (c / 2) * (t = (t * 2 - d) / d) * t * ((_s + 1) * t - _s) + (b + c / 2);
	}
}