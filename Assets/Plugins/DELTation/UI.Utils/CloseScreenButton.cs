using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Screen = DELTation.UI.Screen;

namespace Plugins.DELTation.UI.Utils
{
	[RequireComponent(typeof(Button))]
	public sealed class CloseScreenButton : MonoBehaviour
	{
		private void OnEnable()
		{
			_button.onClick.AddListener(_onClick);
		}

		private void OnDisable()
		{
			_button.onClick.RemoveListener(_onClick);
		}

		private void Awake()
		{
			_button = GetComponent<Button>();
			_screen = GetComponentInParent<Screen>();
			_onClick = () => _screen.Close();
		}

		private Button _button;
		private Screen _screen;
		private UnityAction _onClick;
	}
}