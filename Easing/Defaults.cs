// SPDX-License-Identifier: Apache-2.0
// © 2024-2025 Nikolay Melnikov <n.melnikov@depra.org>

using System;

namespace Depra.Easing
{
	internal static class Defaults
	{
		public const float OVERSHOOT = 1.70158f;
		public const float S2 = OVERSHOOT * 1.525f;
		public const float S3 = OVERSHOOT + 1f;
		public const float S4 = 2f * MathF.PI / 3f;
		public const float S5 = 2f * MathF.PI / 4.5f;
	}
}