using System;
using System.Collections.Generic;
using System.Linq;
using DELTation.UI.Screens.Raycasts;
using UnityEngine;

namespace DELTation.UI.Screens
{
    [DisallowMultipleComponent]
    public sealed class GameScreen : MonoBehaviour, IScreenListener, IGameScreen
    {
        [SerializeField] private bool _closeOnStart = true;
        [SerializeField] private bool _blockRaycastsWhenClosed = true;
        [SerializeField] private bool _independentFromParentScreen;

        private bool? _isOpened;
        private IScreenListener[] _listeners;
        private IRaycastBlocker _raycastBlocker;

        private bool ObjectIsActive
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        private IRaycastBlocker RaycastBlocker =>
            _raycastBlocker ?? (_blockRaycastsWhenClosed
                ? _raycastBlocker = Raycasts.RaycastBlocker.CreateAt(transform)
                : new NullRaycastBlocker());

        private IScreenListener[] Listeners => _listeners ??= GetChildrenListeners(transform).ToArray();

        private void Awake()
        {
            if (_closeOnStart)
            {
                if (_isOpened != true)
                    CloseImmediately();
            }
            else
            {
                _isOpened = ObjectIsActive;
            }
        }

        private void Update()
        {
            var shouldBeAwaited = false;

            foreach (var listener in Listeners)
            {
                listener.OnUpdate(this, Time.unscaledDeltaTime);

                if (listener.ShouldBeAwaited)
                    shouldBeAwaited = true;
            }

            if (!IsOpened && !shouldBeAwaited)
                ObjectIsActive = false;
        }

        public bool IsOpened => _isOpened ?? false;

        public void Open()
        {
            if (_isOpened == true) return;

            ObjectIsActive = true;
            _isOpened = true;
            if (!ObjectIsActive)
                ObjectIsActive = true;
            _isOpened = true;

            RaycastBlocker.Active = false;
            transform.SetAsLastSibling();
            OnOpened();
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

        public event EventHandler Closed;

        public void CloseImmediately()
        {
            Close();
            ObjectIsActive = false;
            OnClosedImmediately();
        }

        public event EventHandler ClosedImmediately;

        public bool ShouldBeAwaited
        {
            get
            {
                if (!gameObject.activeSelf) return false;

                foreach (var listener in Listeners)
                {
                    if (listener.ShouldBeAwaited)
                        return true;
                }

                return false;
            }
        }

        void IScreenListener.OnUpdate(IGameScreen gameScreen, float deltaTime) { }

        void IScreenListener.OnOpened(IGameScreen gameScreen) => Open();

        void IScreenListener.OnClosed(IGameScreen gameScreen) => Close();

        void IScreenListener.OnClosedImmediately(IGameScreen gameScreen) => CloseImmediately();

        public IReadOnlyList<IScreenListener> GetChildListeners() => Listeners;

        private void OnOpened()
        {
            foreach (var listener in _listeners)
            {
                listener.OnOpened(this);
            }

            Opened?.Invoke(this, EventArgs.Empty);
        }

        private void OnClosed()
        {
            foreach (var listener in Listeners)
            {
                listener.OnClosed(this);
            }

            Closed?.Invoke(this, EventArgs.Empty);
        }

        private void OnClosedImmediately()
        {
            foreach (var listener in Listeners)
            {
                listener.OnClosedImmediately(this);
            }

            ClosedImmediately?.Invoke(this, EventArgs.Empty);
        }

        private IEnumerable<IScreenListener> GetChildrenListeners(Transform root)
        {
            if (root.TryGetComponent(out GameScreen subscreen) && !ReferenceEquals(this, subscreen))
            {
                if (!subscreen._independentFromParentScreen)
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
    }
}