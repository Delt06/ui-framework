﻿using DELTation.UI.Screens;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Scripts
{
    [RequireComponent(typeof(Button))]
    public sealed class OpenScreenButton : MonoBehaviour
    {
        [SerializeField] private GameScreen _gameScreen;

        private Button _button;
        private UnityAction _onClick;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _onClick = () => _gameScreen.Open();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(_onClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(_onClick);
        }
    }
}