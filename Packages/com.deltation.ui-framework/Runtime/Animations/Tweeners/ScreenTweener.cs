using DELTation.UI.Easing;
using UnityEngine;

namespace DELTation.UI.Animations.Tweeners
{
    public abstract class ScreenTweener<T> : IScreenTweener
    {
        public readonly TweenStateData<T> CloseData;
        public readonly TweenStateData<T> OpenData;
        private float _currentDelay;
        private float _currentDuration;
        private EaseMode _currentEase;
        private float _currentOvershoot;
        private float _currentTime;

        private T _fromState;
        private T _toState;

        protected ScreenTweener(T openState, T closedState)
        {
            OpenData = new TweenStateData<T>(openState);
            CloseData = new TweenStateData<T>(closedState);
        }

        public bool IsActive { get; private set; }

        protected abstract T CurrentState { get; set; }

        public void Open()
        {
            LoadFrom(OpenData);
            _fromState = CurrentState;
        }

        public void Close()
        {
            LoadFrom(CloseData);
            _fromState = CurrentState;
        }

        public void CloseImmediately()
        {
            IsActive = false;
            CurrentState = CloseData.State;
        }

        public void Update(float deltaTime)
        {
            if (!IsActive) return;

            _currentTime += deltaTime;
            UpdateState();
            if (_currentTime < _currentDuration + _currentDelay) return;

            IsActive = false;
        }

        private void LoadFrom(TweenStateData<T> data)
        {
            IsActive = true;
            _currentTime = 0f;
            (_currentDelay, _currentDuration, _currentEase, _currentOvershoot, _toState) = data;
        }

        private void UpdateState()
        {
            if (_currentTime < _currentDelay) return;

            var timeAfterDelay = _currentTime - _currentDelay;
            var t = Mathf.Clamp01(timeAfterDelay / _currentDuration);
            CurrentState = Interpolate(_fromState, _toState, t);
        }

        private T Interpolate(T value1, T value2, float t)
        {
            var linearT = _currentEase.GetValue(t, _currentOvershoot);
            return InterpolateValuesUnclamped(value1, value2, linearT);
        }

        protected abstract T InterpolateValuesUnclamped(T value1, T value2, float t);
    }
}