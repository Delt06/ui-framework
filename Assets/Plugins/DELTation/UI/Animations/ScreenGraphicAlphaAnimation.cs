using DELTation.UI.Animations.Tweeners.Properties.Elements;
using UnityEngine.UI;

namespace DELTation.UI.Animations
{
	public sealed class ScreenGraphicAlphaAnimation : ScreenAlphaAnimation
	{
		protected override ITransparentElement CreateTransparentElement() =>
			new GraphicTransparentElement(GetComponent<Graphic>());
	}
}