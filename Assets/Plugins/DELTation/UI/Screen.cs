using System;
using UnityEngine;

namespace DELTation.UI
{
	public sealed class Screen : MonoBehaviour
	{
		public bool IsOpened => _isOpened ?? false;

		public void Open()
		{
			if (_isOpened == true) return;

			ObjectIsActive = true;
			_isOpened = true;
			_raycastBlocker.Active = false;
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
			_raycastBlocker.Active = true;
			transform.SetAsFirstSibling();
			OnClosed();
		}

		private void OnClosed()
		{
			foreach (var listener in _listeners)
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
			foreach (var listener in _listeners)
			{
				listener.OnClosedImmediately();
			}

			ClosedImmediately?.Invoke(this, EventArgs.Empty);
		}

		public event EventHandler ClosedImmediately;

		private void Update()
		{
			var anyWorking = false;

			foreach (var listener in _listeners)
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
			_raycastBlocker = RaycastBlocker.CreateAt(transform);
			_listeners = GetComponentsInChildren<IScreenListener>();
			CloseImmediately();
		}

		private bool? _isOpened;
		private RaycastBlocker _raycastBlocker;
		private IScreenListener[] _listeners;
	}
}