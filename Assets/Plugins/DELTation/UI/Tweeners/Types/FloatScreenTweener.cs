using UnityEngine;

namespace DELTation.UI.Tweeners.Types
{
	public abstract class FloatScreenTweener : ScreenTweener<float>
	{
		protected FloatScreenTweener(float openState, float closedState) : base(openState, closedState) { }

		protected override float LinearlyInterpolateUnclamped(float value1, float value2, float t) =>
			Mathf.LerpUnclamped(value1, value2, t);
	}
}