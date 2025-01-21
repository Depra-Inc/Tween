namespace Depra.Tween.Tweening
{
	internal struct TweenGroupData
	{
		public static TweenGroupData Map(TweenSettings settings) => new()
		{
			_lifeTime = 0,
			_delay = settings.Delay,
			_step = 1 / settings.Duration
		};

		public bool CanAnimate;
		public float PlaybackTime;

		private float _step;
		private float _delay;
		private float _lifeTime;

		public void Calculate(float deltaTime)
		{
			if (_delay > 0)
			{
				_delay -= deltaTime;
				return;
			}

			CanAnimate = true;
			_lifeTime += deltaTime;
			PlaybackTime = _lifeTime * _step;
		}
	}
}