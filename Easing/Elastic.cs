// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

using System;

namespace Depra.Tween.Easing
{
	public static class Elastic
	{
		public static readonly IEasing EASE_IN;
		public static readonly IEasing EASE_OUT;
		public static readonly IEasing EASE_IN_OUT;
		public static readonly IEasing EASE_OUT_IN;

		static Elastic()
		{
			EASE_IN = new ElasticEaseIn();
			EASE_OUT = new ElasticEaseOut();
			EASE_IN_OUT = new ElasticEaseInOut();
			EASE_OUT_IN = new ElasticEaseOutIn();
		}
	}

	public sealed record ElasticEaseIn : IEasing
	{
		private float _a;
		private float _p;

		public ElasticEaseIn(float a = 0f, float p = 0f)
		{
			_a = a;
			_p = p;
		}

		public float Calculate(float t, float b, float c, float d)
		{
			if (t == 0f)
			{
				return b;
			}

			if ((t /= d) == 1f)
			{
				return b + c;
			}

			if (_p == 0f)
			{
				_p = d * 0.3f;
			}

			float s;
			if (_a == 0f || _a < Math.Abs(c))
			{
				_a = c;
				s = _p / 4f;
			}
			else
			{
				s = _p / (2f * (float) Math.PI) * (float) Math.Asin(c / _a);
			}

			return -(_a * (float) Math.Pow(2f, 10f * (t -= 1f)) * (float) Math.Sin((t * d - s) * (2f * Math.PI) / _p)) + b;
		}
	}

	public sealed record ElasticEaseInOut : IEasing
	{
		private float _a;
		private float _p;

		public ElasticEaseInOut(float a = 0f, float p = 0f)
		{
			_a = a;
			_p = p;
		}

		public float Calculate(float t, float b, float c, float d)
		{
			if (t == 0f)
			{
				return b;
			}

			if ((t /= d / 2f) == 2f)
			{
				return b + c;
			}

			if (_p == 0f)
			{
				_p = d * (0.3f * 1.5f);
			}

			float s;
			if (_a == 0f || _a < Math.Abs(c))
			{
				_a = c;
				s = _p / 4f;
			}
			else
			{
				s = _p / (2f * (float) Math.PI) * (float) Math.Asin(c / _a);
			}

			return t < 1f
				? -0.5f * (_a * (float) Math.Pow(2f, 10f * (t -= 1)) *
				           (float) Math.Sin((t * d - s) * (2f * (float) Math.PI) / _p)) + b
				: _a * (float) Math.Pow(2f, -10f * (t -= 1f)) *
				(float) Math.Sin((t * d - s) * (2f * (float) Math.PI) / _p) * 0.5f + c + b;
		}
	}

	public sealed record ElasticEaseOut : IEasing
	{
		private float _a;
		private float _p;

		public ElasticEaseOut(float a = 0f, float p = 0f)
		{
			_a = a;
			_p = p;
		}

		public float Calculate(float t, float b, float c, float d)
		{
			if (t == 0)
			{
				return b;
			}

			if ((t /= d) == 1f)
			{
				return b + c;
			}

			if (_p == 0f)
			{
				_p = d * 0.3f;
			}

			float s;
			if (_a == 0f || _a < Math.Abs(c))
			{
				_a = c;
				s = _p / 4f;
			}
			else
			{
				s = _p / (2f * (float) Math.PI) * (float) Math.Asin(c / _a);
			}

			return _a * (float) Math.Pow(2f, -10f * t) *
				(float) Math.Sin((t * d - s) * (2f * (float) Math.PI) / _p) + c + b;
		}
	}

	public sealed record ElasticEaseOutIn : IEasing
	{
		private float _a;
		private float _p;

		public ElasticEaseOutIn(float a = 0f, float p = 0f)
		{
			_a = a;
			_p = p;
		}

		public float Calculate(float t, float b, float c, float d)
		{
			float s;
			c /= 2f;

			if (t < d / 2f)
			{
				if ((t *= 2f) == 0f)
				{
					return b;
				}

				if ((t /= d) == 1f)
				{
					return b + c;
				}

				if (_p == 0f)
				{
					_p = d * 0.3f;
				}

				if (_a == 0f || _a < Math.Abs(c))
				{
					_a = c;
					s = _p / 4f;
				}
				else
				{
					s = _p / (2f * (float) Math.PI) * (float) Math.Asin(c / _a);
				}

				return _a * (float) Math.Pow(2f, -10f * t) *
					(float) Math.Sin((t * d - s) * (2f * (float) Math.PI) / _p) + c + b;
			}

			if ((t = t * 2f - d) == 0f)
			{
				return (b + c);
			}

			if ((t /= d) == 1f)
			{
				return (b + c) + c;
			}

			if (_p == 0f)
			{
				_p = d * 0.3f;
			}

			if (_a == 0f || _a < Math.Abs(c))
			{
				_a = c;
				s = _p / 4f;
			}
			else
			{
				s = _p / (2f * (float) Math.PI) * (float) Math.Asin(c / _a);
			}

			return -(_a * (float) Math.Pow(2f, 10f * (t -= 1f)) *
			         (float) Math.Sin((t * d - s) * (2f * (float) Math.PI) / _p)) + (b + c);
		}
	}
}