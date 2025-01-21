using System;
using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;
using Leopotam.Easings;

namespace Depra.Easing.Benchmarks
{
	public class EasingBenchmarks
	{
		private readonly float _value = new Random().Next();
		private readonly EaseQuadOutStruct _easingStruct = new();
		private readonly IEasingNew _easing = new EaseQuadOutInterface();
		private readonly IEasingNew _easingClass = new EaseQuadOutClass();
		private readonly EasingSwitch _easingSwitch = new(new EaseQuadOutStruct(), new EaseQuadOutStruct());

		[Benchmark(Baseline = true)]
		public float EaseQuadOut() => Ease.QuadOut.Raw(_value);

		[Benchmark]
		public float EaseQuadOut_Interface() => _easing.Calculate(_value);

		[Benchmark]
		public float EaseQuatOut_Struct() => _easingStruct.Calculate(_value);

		[Benchmark]
		public float EaseQuatOut_Class() => _easingClass.Calculate(_value);

		[Benchmark]
		public float EaseQuatOut_Switch() => _easingSwitch.Calculate(_value);

		private sealed class EasingSwitch
		{
			private readonly IEasingNew _easing1;
			private readonly IEasingNew _easing2;
			private Data _data;

			public EasingSwitch(IEasingNew easing1, IEasingNew easing2)
			{
				_data = new Data
				{
					Ease = easing1
				};
				_easing1 = easing1;
				_easing2 = easing2;
			}

			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public float Calculate(float value)
			{
				var result = _data.Ease.Calculate(value);
				_data.Ease = _data.Switched ? _easing1 : _easing2;
				_data.Switched = _data.Switched == false;

				return result;
			}

			private struct Data
			{
				public bool Switched;
				public IEasingNew Ease;
			}
		}

		private interface IEasingNew
		{
			float Calculate(float a);
		}

		private readonly struct EaseQuadOutStruct : IEasingNew
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public float Calculate(float value) => -value * (value - 2f);
		}

		private sealed class EaseQuadOutClass : IEasingNew
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public float Calculate(float value) => -value * (value - 2f);
		}

		private readonly struct EaseQuadOutInterface : IEasingNew
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			float IEasingNew.Calculate(float value) => -value * (value - 2f);
		}
	}
}