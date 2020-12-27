using System;
using DELTation.UI.Animations.Tweeners.Types;
using JetBrains.Annotations;
using UnityEngine;

namespace DELTation.UI.Animations.Tweeners.Properties
{
	public sealed class LocalPositionScreenTweener : Vector3ScreenTweener
	{
		public LocalPositionScreenTweener([NotNull] Transform transform, Vector3 openState, Vector3 closedState) :
			base(openState, closedState) =>
			_transform = transform ? transform : throw new ArgumentNullException(nameof(transform));

		protected override Vector3 CurrentState
		{
			get => _transform.localPosition;
			set => _transform.localPosition = value;
		}

		private readonly Transform _transform;
	}
}