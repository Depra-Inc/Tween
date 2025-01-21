// SPDX-License-Identifier: Apache-2.0
// © 2024-2025 Nikolay Melnikov <n.melnikov@depra.org>

namespace Depra.Easing
{
	public static class Linear
	{
		public static readonly IEase NONE = new EaseNone();
	}

	public readonly struct EaseNone : IEase
	{
		float IEase.Calculate(float t) => t;

		float IEase.Calculate(float t, float b, float c, float d) => b + c * t / d;
	}
}