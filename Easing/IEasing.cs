// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

namespace Depra.Easing
{
	public interface IEasing
	{
		/// <summary>
		/// Calculates Robert Penner's easing.
		/// </summary>
		/// <param name="t">Time.</param>
		/// <param name="b">Beginning value.</param>
		/// <param name="c">Value delta.</param>
		/// <param name="d">Duration.</param>
		float Calculate(float t, float b, float c, float d = 1f);
	}
}