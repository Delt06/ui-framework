using System;
using Cysharp.Threading.Tasks;
using DELTation.UI.Screens;
using DELTation.UI.UniTaskSupport;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts
{
    public class AsyncGameScreen : MonoBehaviour
    {
        [SerializeField] private GameScreen _gameScreen;
        [SerializeField] private Button _openNextButton;
        [SerializeField] private GameScreen _nextGameScreen;

        public void Open() => OpenAsync().Forget();

        private async UniTask OpenAsync()
        {
            await _gameScreen.OpenAsync();
            await _openNextButton.OnClickAsync();
            await _nextGameScreen.OpenAsync();
            await UniTask.Delay(TimeSpan.FromSeconds(1f));
            await _nextGameScreen.CloseAsync();
            await _gameScreen.CloseAsync();
        }
    }
}