namespace DELTation.UI
{
	public interface IScreenListener
	{
		bool IsWorking { get; }
		void OnUpdate(float deltaTime);
		void OnOpened();
		void OnClosed();
		void OnClosedImmediately();
	}
}