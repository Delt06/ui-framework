namespace DELTation.UI.Screens
{
    public interface IScreenListener : IAwaitableAnimation
    {
        void OnUpdate(IGameScreen gameScreen, float deltaTime);
        void OnOpened(IGameScreen gameScreen);
        void OnClosed(IGameScreen gameScreen);
        void OnClosedImmediately(IGameScreen gameScreen);
    }
}