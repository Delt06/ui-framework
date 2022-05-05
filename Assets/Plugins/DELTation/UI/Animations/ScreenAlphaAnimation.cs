using DELTation.UI.Animations.Tweeners;
using DELTation.UI.Animations.Tweeners.Properties;
using DELTation.UI.Animations.Tweeners.Properties.Elements;

namespace DELTation.UI.Animations
{
    public abstract class ScreenAlphaAnimation : ScreenAnimation<float, float>
    {
        private ITransparentElement _transparentElement;

        private ITransparentElement TransparentElement =>
            _transparentElement ?? (_transparentElement = CreateTransparentElement());

        protected override ScreenTweener<float> CreateTweener(float? openState, float closedState) =>
            new AlphaScreenTweener(TransparentElement, openState, closedState);

        protected abstract ITransparentElement CreateTransparentElement();
    }
}