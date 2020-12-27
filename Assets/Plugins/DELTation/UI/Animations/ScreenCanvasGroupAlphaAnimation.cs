using DELTation.UI.Tweeners.Properties.Elements;
using UnityEngine;

namespace DELTation.UI.Animations
{
	public sealed class ScreenCanvasGroupAlphaAnimation : ScreenAlphaAnimation
	{
		protected override ITransparentElement CreateTransparentElement() =>
			new CanvasGroupTransparentElement(GetComponent<CanvasGroup>());
	}
}