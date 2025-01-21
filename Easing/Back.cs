// SPDX-License-Identifier: Apache-2.0
// © 2024-2025 Nikolay Melnikov <n.melnikov@depra.org>

using System;

namespace Depra.Easing
{
	public static class Back
	{
		public static readonly IEase IN;
		public static readonly IEase OUT;
		public static readonly IEase IN_OUT;
		public static readonly IEase OUT_IN;

		static Back()
		{
			IN = new BackEaseIn();
			OUT = new BackEaseOut();
			IN_OUT = new BackEaseInOut();
			OUT_IN = new BackEaseOutIn();
		}
	}

	public sealed record BackEaseIn : IEase
	{
		private readonly float _s;
		private readonly float _s3;

		public BackEaseIn() : this(Defaults.OVERSHOOT, Defaults.S3) { }
		public BackEaseIn(float s, float s3)
		{
			_s = s;
			_s3 = s3;
		}

		public float Calculate(float t) => _s3 * t * t * t - _s * t * t;

		public float Calculate(float t, float b, float c, float d) => c * (t /= d) * t * ((_s + 1) * t - _s) + b;
	}

	public sealed record BackEaseOut : IEase
	{
		private readonly float _s;
		private readonly float _s3;

		public BackEaseOut() : this(Defaults.OVERSHOOT, Defaults.S3) { }
		public BackEaseOut(float s, float s3)
		{
			_s = s;
			_s3 = s3;
		}

		public float Calculate(float t) =>
			1.0f + _s3 * MathF.Pow(t - 1.0f, 3.0f) + _s * MathF.Pow(t - 1.0f, 2.0f);

		public float Calculate(float t, float b, float c, float d) =>
			c * ((t = t / d - 1) * t * ((_s + 1) * t + _s) + 1) + b;
	}

	public sealed record BackEaseInOut : IEase
	{
		private readonly float _s;
		private readonly float _s2;

		public BackEaseInOut() : this(Defaults.OVERSHOOT, Defaults.S2) { }
		public BackEaseInOut(float s, float s2)
		{
			_s = s;
			_s2 = s2;
		}

		public float Calculate(float t) => t < 0.5f
			? MathF.Pow(2.0f * t, 2.0f) * ((_s2 + 1.0f) * 2.0f * t - _s2) / 2.0f
			: (MathF.Pow(2.0f * t - 2.0f, 2.0f) * ((_s2 + 1.0f) * (t * 2.0f - 2.0f) + _s2) + 2.0f) / 2.0f;

		public float Calculate(float t, float b, float c, float d) => (t /= d / 2f) < 1f
			? c / 2f * (t * t * (((_s * 1.525f) + 1f) * t - _s * 1.525f)) + b
			: c / 2f * ((t -= 2f) * t * ((_s * 1.525f + 1f) * t + _s * 1.525f) + 2f) + b;
	}

	public sealed record BackEaseOutIn : IEase
	{
		private readonly float _s;

		public BackEaseOutIn() : this(Defaults.OVERSHOOT) { }
		public BackEaseOutIn(float s) => _s = s;

		public float Calculate(float t) => throw new NotImplementedException();

		public float Calculate(float t, float b, float c, float d) => t < d / 2
			? (c / 2) * ((t = (t * 2) / d - 1) * t * ((_s + 1) * t + _s) + 1) + b
			: (c / 2) * (t = (t * 2 - d) / d) * t * ((_s + 1) * t - _s) + (b + c / 2);
	}
}