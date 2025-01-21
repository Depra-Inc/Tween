namespace Depra.Tween.Tweening
{
	public interface ITween
	{
		TweenState State { get; }

		void Update(float time);
	}

	public enum TweenState
	{
		PLAYING,
		PAUSED,
		STOPPED
	}

	public enum TweenLoop
	{
		ONCE,
		LOOP,
		PING_PONG
	}
}