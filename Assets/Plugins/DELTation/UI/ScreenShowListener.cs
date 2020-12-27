using UnityEngine;

namespace DELTation.UI
{
	public abstract class ScreenListener : MonoBehaviour, IScreenListener
	{
		public abstract bool IsWorking { get; }
		public virtual void OnUpdate(float deltaTime) { }

		public virtual void OnOpened() { }

		public virtual void OnClosed() { }

		public virtual void OnClosedImmediately() { }
	}
}