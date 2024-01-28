// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

using System;

namespace Depra.Easing
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
		public float A;
		public float P;

		public ElasticEaseIn() : this(0, 0) { }

		public ElasticEaseIn(float a, float p)
		{
			A = a;
			P = p;
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

			if (P == 0f)
			{
				P = d * 0.3f;
			}

			float s;
			if (A == 0f || A < Math.Abs(c))
			{
				A = c;
				s = P / 4f;
			}
			else
			{
				s = P / (2f * (float) Math.PI) * (float) Math.Asin(c / A);
			}

			return -(A * (float) Math.Pow(2f, 10f * (t -= 1f)) * (float) Math.Sin((t * d - s) * (2f * Math.PI) / P)) +
			       b;
		}
	}

	public sealed record ElasticEaseInOut : IEasing
	{
		public float A;
		public float P;

		public ElasticEaseInOut() : this(0, 0) { }

		public ElasticEaseInOut(float a, float p)
		{
			A = a;
			P = p;
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

			if (P == 0f)
			{
				P = d * (0.3f * 1.5f);
			}

			float s;
			if (A == 0f || A < Math.Abs(c))
			{
				A = c;
				s = P / 4f;
			}
			else
			{
				s = P / (2f * (float) Math.PI) * (float) Math.Asin(c / A);
			}

			return t < 1f
				? -0.5f * (A * (float) Math.Pow(2f, 10f * (t -= 1)) *
				           (float) Math.Sin((t * d - s) * (2f * (float) Math.PI) / P)) + b
				: A * (float) Math.Pow(2f, -10f * (t -= 1f)) *
				(float) Math.Sin((t * d - s) * (2f * (float) Math.PI) / P) * 0.5f + c + b;
		}
	}

	public sealed record ElasticEaseOut : IEasing
	{
		public float A;
		public float P;

		public ElasticEaseOut() : this(0, 0) { }

		public ElasticEaseOut(float a, float p)
		{
			A = a;
			P = p;
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

			if (P == 0f)
			{
				P = d * 0.3f;
			}

			float s;
			if (A == 0f || A < Math.Abs(c))
			{
				A = c;
				s = P / 4f;
			}
			else
			{
				s = P / (2f * (float) Math.PI) * (float) Math.Asin(c / A);
			}

			return A * (float) Math.Pow(2f, -10f * t) *
				(float) Math.Sin((t * d - s) * (2f * (float) Math.PI) / P) + c + b;
		}
	}

	public sealed record ElasticEaseOutIn : IEasing
	{
		public float A;
		public float P;

		public ElasticEaseOutIn() : this(0, 0) { }

		public ElasticEaseOutIn(float a, float p)
		{
			A = a;
			P = p;
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

				if (P == 0f)
				{
					P = d * 0.3f;
				}

				if (A == 0f || A < Math.Abs(c))
				{
					A = c;
					s = P / 4f;
				}
				else
				{
					s = P / (2f * (float) Math.PI) * (float) Math.Asin(c / A);
				}

				return A * (float) Math.Pow(2f, -10f * t) *
					(float) Math.Sin((t * d - s) * (2f * (float) Math.PI) / P) + c + b;
			}

			if ((t = t * 2f - d) == 0f)
			{
				return (b + c);
			}

			if ((t /= d) == 1f)
			{
				return (b + c) + c;
			}

			if (P == 0f)
			{
				P = d * 0.3f;
			}

			if (A == 0f || A < Math.Abs(c))
			{
				A = c;
				s = P / 4f;
			}
			else
			{
				s = P / (2f * (float) Math.PI) * (float) Math.Asin(c / A);
			}

			return -(A * (float) Math.Pow(2f, 10f * (t -= 1f)) *
			         (float) Math.Sin((t * d - s) * (2f * (float) Math.PI) / P)) + (b + c);
		}
	}
}