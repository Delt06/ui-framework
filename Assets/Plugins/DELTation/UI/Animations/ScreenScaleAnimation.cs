using DELTation.UI.Tweeners;
using DELTation.UI.Tweeners.Properties;
using UnityEngine;

namespace DELTation.UI.Animations
{
	public sealed class ScreenScaleAnimation : ScreenAnimation<Vector3>
	{
		protected override ScreenTweener<Vector3> CreateTweener(Vector3 openState, Vector3 closedState) =>
			new ScaleScreenTweener(transform, openState, closedState);

		protected override Vector3 GetInitialState() => transform.localScale;
	}
}