// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

namespace Depra.Easing
{
	public static class Linear
	{
		public static readonly IEasing NONE = new EaseNone();
	}

	public readonly struct EaseNone : IEasing
	{
		float IEasing.Calculate(float t, float b, float c, float d) => b + c * t / d;
	}
}