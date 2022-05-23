namespace DELTation.UI.Screens
{
    public abstract class OpenScreenBehaviour : AwaitableAnimationBase, IScreenListener
    {
        public override bool ShouldBeAwaited => false;

        void IScreenListener.OnUpdate(IGameScreen gameScreen, float deltaTime)
        {
            if (!gameScreen.IsOpened) return;
            OnUpdate(deltaTime);
        }

        void IScreenListener.OnOpened(IGameScreen gameScreen) { }

        void IScreenListener.OnClosed(IGameScreen gameScreen) { }
        void IScreenListener.OnClosedImmediately(IGameScreen gameScreen) { }

        protected abstract void OnUpdate(float deltaTime);
    }
}