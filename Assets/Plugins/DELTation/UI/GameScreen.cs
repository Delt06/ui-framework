using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DELTation.UI
{
	[DisallowMultipleComponent]
	public sealed class GameScreen : MonoBehaviour, IScreenListener
	{
		public bool IsOpened => _isOpened ?? false;

		public void Open()
		{
			if (_isOpened == true) return;

			ObjectIsActive = true;
			_isOpened = true;

			if (!ObjectIsActive)
				ObjectIsActive = true;

			RaycastBlocker.Active = false;
			transform.SetAsLastSibling();
			OnOpened();
		}

		private bool ObjectIsActive
		{
			get => gameObject.activeSelf;
			set => gameObject.SetActive(value);
		}

		private void OnOpened()
		{
			foreach (var listener in _listeners)
			{
				listener.OnOpened();
			}

			Opened?.Invoke(this, EventArgs.Empty);
		}

		public event EventHandler Opened;

		public void Close()
		{
			if (_isOpened == false) return;
			_isOpened = false;
			RaycastBlocker.Active = true;
			transform.SetAsFirstSibling();
			OnClosed();
		}

		private void OnClosed()
		{
			foreach (var listener in Listeners)
			{
				listener.OnClosed();
			}

			Closed?.Invoke(this, EventArgs.Empty);
		}

		public event EventHandler Closed;

		public void CloseImmediately()
		{
			Close();
			ObjectIsActive = false;
			OnClosedImmediately();
		}

		private void OnClosedImmediately()
		{
			foreach (var listener in Listeners)
			{
				listener.OnClosedImmediately();
			}

			ClosedImmediately?.Invoke(this, EventArgs.Empty);
		}

		public event EventHandler ClosedImmediately;

		private void Update()
		{
			var anyWorking = false;

			foreach (var listener in Listeners)
			{
				if (!listener.IsWorking) continue;
				listener.OnUpdate(Time.unscaledDeltaTime);
				anyWorking = true;
			}

			if (!IsOpened && !anyWorking)
				ObjectIsActive = false;
		}

		private void Awake()
		{
			if (_isOpened != true)
				CloseImmediately();
		}

		private IEnumerable<IScreenListener> GetChildrenListeners(Transform root)
		{
			if (root.TryGetComponent(out GameScreen subscreen) && !ReferenceEquals(this, subscreen))
			{
				yield return subscreen;
				yield break;
			}

			foreach (var listener in root.GetComponents<IScreenListener>())
			{
				if (ReferenceEquals(listener, this)) continue;
				yield return listener;
			}

			foreach (Transform child in root)
			{
				foreach (var screenListener in GetChildrenListeners(child))
				{
					yield return screenListener;
				}
			}
		}

		private RaycastBlocker RaycastBlocker =>
			_raycastBlocker ? _raycastBlocker : _raycastBlocker = RaycastBlocker.CreateAt(transform);

		private IScreenListener[] Listeners => _listeners ?? (_listeners = GetChildrenListeners(transform).ToArray());

		private bool? _isOpened;
		private RaycastBlocker _raycastBlocker;
		private IScreenListener[] _listeners;

		bool IScreenListener.IsWorking => gameObject.activeSelf;

		void IScreenListener.OnUpdate(float deltaTime) { }

		void IScreenListener.OnOpened() => Open();
		void IScreenListener.OnClosed() => Close();

		void IScreenListener.OnClosedImmediately() => CloseImmediately();
	}
}