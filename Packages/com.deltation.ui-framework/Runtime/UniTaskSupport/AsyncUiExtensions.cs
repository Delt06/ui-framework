﻿#if DELTATION_UI_UNITASK
using System;
using Cysharp.Threading.Tasks;
using DELTation.UI.Screens;
using JetBrains.Annotations;

namespace DELTation.UI.UniTaskSupport
{
    public static class AsyncUiExtensions
    {
        public static async UniTask OpenAsync([NotNull] this GameScreen gameScreen)
        {
            if (gameScreen == null) throw new ArgumentNullException(nameof(gameScreen));
            gameScreen.Open();
            await gameScreen.WaitForAnimationEnd();
        }

        public static async UniTask CloseAsync(this GameScreen gameScreen)
        {
            if (gameScreen == null) throw new ArgumentNullException(nameof(gameScreen));
            gameScreen.Close();
            await gameScreen.WaitForAnimationEnd();
        }

        public static async UniTask WaitForAnimationEnd([NotNull] this IAwaitableAnimation awaitableAnimation)
        {
            if (awaitableAnimation == null) throw new ArgumentNullException(nameof(awaitableAnimation));

            while (awaitableAnimation.ShouldBeAwaited)
            {
                await UniTask.Yield();
            }
        }
    }
}
#endif