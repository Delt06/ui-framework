using DELTation.UI.Animations.Tweeners.Properties;
using DELTation.UI.Tweeners;
using UnityEngine;

namespace DELTation.UI.Animations
{
	public sealed class ScreenLocalRotationAnimation : ScreenAnimation<Vector3, Quaternion>
	{
		protected override ScreenTweener<Quaternion> CreateTweener(Vector3 openState, Vector3 closedState) =>
			new LocalRotationScreenTweener(transform, Quaternion.Euler(openState), Quaternion.Euler(closedState));

		protected override Vector3 GetInitialState() => transform.localEulerAngles;
	}
}