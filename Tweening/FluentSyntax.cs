using System.Runtime.CompilerServices;

namespace Depra.Tween.Tweening
{
	public static class FluentSyntax
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static TTween ApplyTween<TTween>(TweenProcessor self, TweenSettings settings)
			where TTween : ITween, new() =>
			self.ApplyTweenGroup().WithSettings(settings).AddTween<TTween>();
	}
}