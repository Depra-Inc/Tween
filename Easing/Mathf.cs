// SPDX-License-Identifier: Apache-2.0
// © 2024-2025 Nikolay Melnikov <n.melnikov@depra.org>

using System.Runtime.CompilerServices;

namespace Depra.Easing
{
	internal static class Mathf
	{
		public const float PI = 3.14159274f;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Pow(float x, float y) => (float)System.Math.Pow(x, y);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Sin(float x) => (float)System.Math.Sin(x);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Asin(float x) => (float)System.Math.Asin(x);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Abs(float x)
		{
			if (x < 0f)
			{
				return -x;
			}

			return x;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Sqrt(float x) => (float)System.Math.Sqrt(x);
	}
}