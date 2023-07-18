using System;
using UnityEngine;

namespace TimeSDK.UI
{
	public class ClosableViewPanel : ViewPanel
	{
		[SerializeField] private ClosePanel _closePanel;

		private void Start()
		{
			_closePanel.OnClose += ClosePanel;
		}

		private void ClosePanel(object sender, EventArgs e)
		{
			Hide();
		}

		private void OnDestroy()
		{
			_closePanel.OnClose -= ClosePanel;
		}
	}
}