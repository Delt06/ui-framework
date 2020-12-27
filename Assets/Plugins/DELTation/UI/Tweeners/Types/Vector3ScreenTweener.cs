using UnityEngine;

namespace DELTation.UI.Tweeners.Types
{
	public abstract class Vector3ScreenTweener : ScreenTweener<Vector3>
	{
		protected Vector3ScreenTweener(Vector3 openState, Vector3 closedState) : base(openState, closedState) { }

		protected override Vector3 LinearlyInterpolateUnclamped(Vector3 value1, Vector3 value2, float t) =>
			Vector3.LerpUnclamped(value1, value2, t);
	}
}