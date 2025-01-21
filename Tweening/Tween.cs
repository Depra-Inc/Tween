using System;
using Depra.Easing;

namespace Depra.Tween.Tweening
{
	public abstract class Tween<T> : ITween where T : struct
	{
		public T Value { get; private set; }
		public float Time { get; private set; }
		public float Progress { get; private set; }
		public int ExecutionCount { get; private set; }
		public TweenState State { get; private set; } = TweenState.PAUSED;

		private T _origin;
		private T _destination;
		private IEase _easeIn = Linear.NONE;
		private IEase _easeOut = Linear.NONE;
		private TweenLoop _loop = TweenLoop.ONCE;

		private bool _clamp;
		private float _duration;
		private float _currentTime;
		private int _residueCount = -1;

		private Action<Tween<T>> _onUpdate;
		private Action<Tween<T>> _onComplete;
		private Func<Tween<T>, bool> _condition;

		private readonly Func<Tween<T>, T, T, float, bool, T> _interpolation;

		protected Tween(Func<Tween<T>, T, T, float, bool, T> interpolation) => _interpolation = interpolation;

		public Tween<T> Origin(T start)
		{
			_origin = start;
			return this;
		}

		public Tween<T> Destination(T end)
		{
			_destination = end;
			return this;
		}

		public Tween<T> Duration(float duration)
		{
			_duration = duration;
			return this;
		}

		public Tween<T> Loop(TweenLoop loop)
		{
			_loop = loop;
			return this;
		}

		public Tween<T> Ease(IEase ease)
		{
			_easeIn = _easeOut = ease;
			return this;
		}

		public Tween<T> EaseIn(IEase ease)
		{
			_easeIn = ease;
			return this;
		}

		public Tween<T> EaseOut(IEase ease)
		{
			_easeOut = ease;
			return this;
		}

		public Tween<T> OnUpdate(Action<Tween<T>> action)
		{
			_onUpdate = action;
			return this;
		}

		public Tween<T> OnComplete(Action<Tween<T>> action)
		{
			_onComplete = action;
			return this;
		}

		public Tween<T> Condition(Func<Tween<T>, bool> condition)
		{
			_condition = condition;
			return this;
		}

		public Tween<T> Clamp(bool clamp)
		{
			_clamp = clamp;
			return this;
		}

		public Tween<T> Start()
		{
			State = TweenState.PLAYING;
			UpdateValue();

			return this;
		}

		public void Pause() => State = TweenState.PAUSED;

		public void Resume() => State = TweenState.PLAYING;

		public void Stop(bool moveToEnd = true)
		{
			if (State == TweenState.STOPPED)
			{
				return;
			}

			State = TweenState.STOPPED;
			if (moveToEnd)
			{
				_currentTime = _duration;
				UpdateValue();
			}

			_onComplete?.Invoke(this);
		}

		public void Reset()
		{
			_currentTime = 0;
			Value = _origin;
		}

		public void Update(float time)
		{
			_currentTime += time;
			if (_currentTime < _duration)
			{
				UpdateValue();
				return;
			}

			_residueCount--;
			ExecutionCount++;
			UpdateLoop();
		}

		private void UpdateLoop()
		{
			switch (_loop)
			{
				case TweenLoop.ONCE:
					Stop();
					break;
				case TweenLoop.LOOP:
					if (_residueCount == 0)
					{
						Stop();
					}

					Value = _origin;
					_currentTime = Progress = 0f;
					break;
				case TweenLoop.PING_PONG:
					if (_residueCount == 0)
					{
						Stop();
					}

					(_destination, _origin) = (_origin, _destination);
					_currentTime = Progress = 0f;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private void UpdateValue()
		{
			var t = _currentTime / _duration;
			var ease = t < 0.5f ? _easeIn : _easeOut;
			Progress = ease.Calculate(t);
			Value = _interpolation(this, _origin, _destination, Progress, _clamp);
			_onUpdate?.Invoke(this);
		}
	}
}