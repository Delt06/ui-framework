using DELTation.UI.Tweeners;
using UnityEngine;

namespace DELTation.UI.Animations
{
	public abstract class ScreenAnimation<T> : MonoBehaviour, IScreenListener
	{
		[SerializeField] private TweenData _openData = default;
		[SerializeField] private bool _openToInitialState = true;
		[SerializeField] private T _openState = default;
		[SerializeField] private TweenData _closeData = default;
		[SerializeField] private T _closedState = default;

		public bool IsWorking => _tweener.IsActive;
		public void OnUpdate(float deltaTime) => Tweener.Update(deltaTime);

		public void OnOpened() => Tweener.Open();

		public void OnClosed() => Tweener.Close();

		public void OnClosedImmediately() => Tweener.CloseImmediately();

		private ScreenTweener<T> Tweener => _tweener ?? (_tweener = ConstructTweener());

		private ScreenTweener<T> ConstructTweener()
		{
			var tweener = CreateTweener(OpenState, ClosedState);
			_openData.CopyTo(tweener.OpenData);
			_closeData.CopyTo(tweener.CloseData);
			return tweener;
		}

		protected abstract ScreenTweener<T> CreateTweener(T openState, T closedState);

		private T OpenState => _openToInitialState ? GetInitialState() : _openState;
		private T ClosedState => _closedState;

		protected abstract T GetInitialState();

		private ScreenTweener<T> _tweener;
	}
}