namespace Depra.Tween.Tweening
{
	public struct TweenSettings
	{
		public float Delay { get; }
		public float Duration { get; }

		public TweenSettings(float duration, float delay = 0f)
		{
			Delay = delay;
			Duration = duration;
		}
	}
}