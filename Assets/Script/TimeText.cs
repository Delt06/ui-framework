using DELTation.UI.Screens;
using UnityEngine;
using UnityEngine.UI;

namespace Script
{
	public sealed class TimeText : OpenScreenBehaviour
	{
		protected override void OnUpdate(float deltaTime)
		{
			_text.text = Time.time.ToString();
		}

		private void Awake()
		{
			_text = GetComponent<Text>();
		}

		private Text _text;
	}
}