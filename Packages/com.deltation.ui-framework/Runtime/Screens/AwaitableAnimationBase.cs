using UnityEngine;

namespace DELTation.UI.Screens
{
    public abstract class AwaitableAnimationBase : MonoBehaviour, IAwaitableAnimation
    {
        public abstract bool ShouldBeAwaited { get; }
    }
}