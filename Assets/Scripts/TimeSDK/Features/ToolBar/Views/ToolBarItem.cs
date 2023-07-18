using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TimeSDK.Features.ToolBar.Views
{
	public class ToolBarItem : MonoBehaviour
	{
		public event EventHandler<ToolBarItemName> OnClick;

		public ToolBarItemName Name => _name;
		public TMP_Text ClockFace => _clockFace;

		[SerializeField] private ToolBarItemName _name;
		[SerializeField] private Button _button;
		[SerializeField] private TMP_Text _clockFace;

		private void Start()
		{
			_button.onClick.AddListener(HandleClick);
		}

		private void HandleClick()
		{
			OnClick?.Invoke(this, _name);
		}

		public void ToggleClockFace(bool isHided)
		{
			_clockFace.gameObject.SetActive(!isHided);
		}

		private void OnDestroy()
		{
			_button.onClick.RemoveListener(HandleClick);
		}
	}
}