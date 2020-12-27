using DELTation.UI.Animations.Tweeners.Properties;
using DELTation.UI.Tweeners;
using UnityEngine;

namespace DELTation.UI.Animations
{
	public sealed class ScreenPositionAnimation : ScreenAnimation<Vector3, Vector3>
	{
		[SerializeField] private bool _multiplyValuesByRectSize = false;

		protected override ScreenTweener<Vector3> CreateTweener(Vector3 openState, Vector3 closedState)
		{
			var rectTransform = (RectTransform) transform;

			if (_multiplyValuesByRectSize)
			{
				var rect = rectTransform.rect;
				openState *= rect.size;
				closedState *= rect.size;
			}

			return new LocalPositionScreenTweener(rectTransform, openState, closedState);
		}

		protected override Vector3 GetInitialState() => transform.localPosition;
	}
}