using DELTation.UI.Screens;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts
{
    public sealed class TimeText : OpenScreenBehaviour
    {
        private Text _text;

        private void Awake()
        {
            _text = GetComponent<Text>();
        }

        protected override void OnUpdate(float deltaTime)
        {
            _text.text = Time.time.ToString("F2");
        }
    }
}