using System;
using DELTation.UI.Tweeners.Properties.Elements;
using DELTation.UI.Tweeners.Types;
using JetBrains.Annotations;
using UnityEngine.UI;

namespace DELTation.UI.Tweeners.Properties
{
	public sealed class AlphaScreenTweener : FloatScreenTweener
	{
		public AlphaScreenTweener(ITransparentElement transparentElement, float openState, float closedState) :
			base(openState, closedState) => _transparentElement =
			transparentElement ?? throw new ArgumentNullException(nameof(transparentElement));

		public AlphaScreenTweener(ITransparentElement transparentElement) : this(transparentElement,
			transparentElement.Alpha, 0f) { }

		protected override float CurrentState
		{
			get => _transparentElement.Alpha;
			set => _transparentElement.Alpha = value;
		}

		private readonly ITransparentElement _transparentElement;
	}
}