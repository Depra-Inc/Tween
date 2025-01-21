// SPDX-License-Identifier: Apache-2.0
// © 2024-2025 Nikolay Melnikov <n.melnikov@depra.org>

using System;

namespace Depra.Easing
{
	public static class Elastic
	{
		public static readonly IEase IN;
		public static readonly IEase OUT;
		public static readonly IEase IN_OUT;
		public static readonly IEase OUT_IN;

		static Elastic()
		{
			IN = new ElasticEaseIn();
			OUT = new ElasticEaseOut();
			IN_OUT = new ElasticEaseInOut();
			OUT_IN = new ElasticEaseOutIn();
		}
	}

	public sealed record ElasticEaseIn : IEase
	{
		public float A;
		public float P;

		public ElasticEaseIn() : this(0, 0) { }

		public ElasticEaseIn(float a, float p)
		{
			A = a;
			P = p;
		}

		public float Calculate(float t) =>
			-MathF.Pow(2f, 10f * t - 10f) * MathF.Sin((t * 10f - 10.75f) * Defaults.S4);

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
			if (A == 0f || A < MathF.Abs(c))
			{
				A = c;
				s = P / 4f;
			}
			else
			{
				s = P / (2f * MathF.PI) * MathF.Asin(c / A);
			}

			return -(A * MathF.Pow(2f, 10f * (t -= 1f)) * MathF.Sin((t * d - s) * (2f * MathF.PI) / P)) + b;
		}
	}

	public sealed record ElasticEaseOut : IEase
	{
		public float A;
		public float P;

		public ElasticEaseOut() : this(0, 0) { }

		public ElasticEaseOut(float a, float p)
		{
			A = a;
			P = p;
		}

		public float Calculate(float t) => MathF.Pow(2f, -10f * t) * MathF.Sin((t * 10f - 0.75f) * Defaults.S4) + 1f;

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
			if (A == 0f || A < MathF.Abs(c))
			{
				A = c;
				s = P / 4f;
			}
			else
			{
				s = P / (2f * MathF.PI) * MathF.Asin(c / A);
			}

			return A * MathF.Pow(2f, -10f * t) * MathF.Sin((t * d - s) * (2f * MathF.PI) / P) + c + b;
		}
	}

	public sealed record ElasticEaseInOut : IEase
	{
		public float A;
		public float P;

		public ElasticEaseInOut() : this(0, 0) { }

		public ElasticEaseInOut(float a, float p)
		{
			A = a;
			P = p;
		}

		public float Calculate(float t) => t < 0.5f
			? -(MathF.Pow(2f, 20f * t - 10f) * MathF.Sin((20f * t - 11.125f) * Defaults.S5)) / 2f
			: MathF.Pow(2f, -20f * t + 10f) * MathF.Sin((20f * t - 11.125f) * Defaults.S5) / 2f + 1f;

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
			if (A == 0f || A < MathF.Abs(c))
			{
				A = c;
				s = P / 4f;
			}
			else
			{
				s = P / (2f * MathF.PI) * MathF.Asin(c / A);
			}

			return t < 1f
				? -0.5f * (A * MathF.Pow(2f, 10f * (t -= 1)) *
				           MathF.Sin((t * d - s) * (2f * MathF.PI) / P)) + b
				: A * MathF.Pow(2f, -10f * (t -= 1f)) *
				MathF.Sin((t * d - s) * (2f * MathF.PI) / P) * 0.5f + c + b;
		}
	}

	public sealed record ElasticEaseOutIn : IEase
	{
		public float A;
		public float P;

		public ElasticEaseOutIn() : this(0, 0) { }

		public ElasticEaseOutIn(float a, float p)
		{
			A = a;
			P = p;
		}

		public float Calculate(float t) => throw new System.NotImplementedException();

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
					s = P / (2f * MathF.PI) * MathF.Asin(c / A);
				}

				return A * MathF.Pow(2f, -10f * t) *
					MathF.Sin((t * d - s) * (2f * MathF.PI) / P) + c + b;
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
				s = P / (2f * MathF.PI) * MathF.Asin(c / A);
			}

			return -(A * MathF.Pow(2f, 10f * (t -= 1f)) *
			         MathF.Sin((t * d - s) * (2f * MathF.PI) / P)) + (b + c);
		}
	}
}