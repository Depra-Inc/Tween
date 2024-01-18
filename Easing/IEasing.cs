// SPDX-License-Identifier: Apache-2.0
// © 2024 Nikolay Melnikov <n.melnikov@depra.org>

namespace Depra.Tween.Easing
{
	public interface IEasing
	{
		float Calculate(float arg0, float arg1, float arg2, float arg3);
	}
}