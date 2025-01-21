using System;
using System.Runtime.CompilerServices;

namespace Depra.Tween.Tweening
{
	public sealed class TweenProcessor
	{
		private const int DEFAULT_START_CAPACITY = 1024 * 8;

		private bool[] _activeMask;
		private TweenGroup[] _tweenGroups;

		public TweenProcessor()
		{
			_tweenGroups = new TweenGroup[DEFAULT_START_CAPACITY];
			for (var index = 0; index < _tweenGroups.Length; index++)
			{
				_tweenGroups[index] = new TweenGroup();
			}

			_activeMask = new bool[DEFAULT_START_CAPACITY];
		}

		public void Update(float deltaTime)
		{
			for (var index = 0; index < _tweenGroups.Length; index++)
			{
				if (_activeMask[index] == false)
				{
					continue;
				}

				var tweenGroup = _tweenGroups[index];
				if (tweenGroup is not { IsRunning: true })
				{
					_activeMask[index] = false;
					continue;
				}

				tweenGroup.Update(deltaTime);
			}
		}

		public TweenGroup ApplyTweenGroup()
		{
			var freeIndex = GetFreeIndex();
			var group = _tweenGroups[freeIndex] ??= new TweenGroup();
			_activeMask[freeIndex] = true;

			return group;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void Extend()
		{
			var groups = new TweenGroup[_tweenGroups.Length * 2];
			Array.Copy(_tweenGroups, groups, _tweenGroups.Length);
			_tweenGroups = groups;

			var mask = new bool[_activeMask.Length * 2];
			Array.Copy(_activeMask, mask, _activeMask.Length);
			_activeMask = mask;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private int GetFreeIndex()
		{
			for (var index = 0; index < _tweenGroups.Length; index++)
			{
				if (_activeMask[index] == false)
				{
					return index;
				}
			}

			var nextFreeIndex = _tweenGroups.Length;
			Extend();

			return nextFreeIndex;
		}
	}
}