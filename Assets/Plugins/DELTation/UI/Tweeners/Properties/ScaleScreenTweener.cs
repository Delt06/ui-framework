﻿using System;
using DELTation.UI.Tweeners.Types;
using UnityEngine;

namespace DELTation.UI.Tweeners.Properties
{
	public sealed class ScaleScreenTweener : Vector3ScreenTweener
	{
		public ScaleScreenTweener(Transform transform, Vector3 openState, Vector3 closedState) :
			base(openState, closedState) =>
			_transform = transform ? transform : throw new ArgumentNullException(nameof(transform));

		public ScaleScreenTweener(Transform transform) : this(transform, transform.localScale, Vector3.zero) { }

		protected override Vector3 CurrentState
		{
			get => _transform.localScale;
			set => _transform.localScale = value;
		}

		private readonly Transform _transform;
	}
}