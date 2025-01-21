using System;
using System.Collections.Generic;

namespace Depra.Tween.Tweening
{
	public class TweenGroup
	{
		private readonly Dictionary<Type, ITween> _tweenCache = new();

		private Action _callback;
		private TweenGroupData _data;

		public bool IsRunning { get; private set; }

		public TweenGroup WithCallback(Action callback)
		{
			_callback = callback;
			return this;
		}

		public TweenGroup WithSettings(TweenSettings settings)
		{
			ResetActives();
			_data = TweenGroupData.Map(settings);

			return this;
		}

		public TTween AddTween<TTween>() where TTween : ITween, new()
		{
			var type = typeof(TTween);
			if (_tweenCache.TryGetValue(type, out var tween) == false)
			{
				tween = new TTween();
				_tweenCache.Add(type, tween);
			}

			IsRunning = true;

			return (TTween) tween;
		}

		public void Update(float deltaTime)
		{
			_data.Calculate(deltaTime);
			if (_data.CanAnimate == false)
			{
				return;
			}

			foreach (var tween in _tweenCache.Values)
			{
				if (tween.State == TweenState.PLAYING)
				{
					tween.Update(_data.PlaybackTime);
				}
			}

			if (_data.PlaybackTime >= 1)
			{
				IsRunning = false;
				_callback?.Invoke();
			}
		}

		private void ResetActives()
		{
			foreach (var tweenValue in _tweenCache.Values)
			{
				//tweenValue.IsActive = false;
			}
		}
	}
}