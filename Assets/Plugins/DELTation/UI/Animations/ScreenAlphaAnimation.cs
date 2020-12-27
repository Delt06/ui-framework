using DELTation.UI.Tweeners;
using DELTation.UI.Tweeners.Properties;
using DELTation.UI.Tweeners.Properties.Elements;

namespace DELTation.UI.Animations
{
	public abstract class ScreenAlphaAnimation : ScreenAnimation<float>
	{
		protected override ScreenTweener<float> CreateTweener(float openState, float closedState) =>
			new AlphaScreenTweener(TransparentElement, openState, closedState);

		protected override float GetInitialState() => TransparentElement.Alpha;

		private ITransparentElement TransparentElement =>
			_transparentElement ?? (_transparentElement = CreateTransparentElement());

		protected abstract ITransparentElement CreateTransparentElement();

		private ITransparentElement _transparentElement;
	}
}