using System;
using UnityEngine;
using UnityEngine.UI;

namespace TimeSDK.UI
{
	public class ClosePanel : MonoBehaviour
	{
		public event EventHandler OnClose;

		[SerializeField] private Button _closeButton;

		private void Start()
		{
			_closeButton.onClick.AddListener(PanelClosed);
		}

		private void PanelClosed()
		{
			OnClose?.Invoke(this, EventArgs.Empty);
		}

		private void OnDestroy()
		{
			_closeButton.onClick.RemoveListener(PanelClosed);
		}
	}
}