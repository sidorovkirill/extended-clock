using System;
using UnityEngine;

namespace TimeSDK.UI
{
	public class ViewPanel : MonoBehaviour, IViewPanel
	{
		public void Show()
		{
			gameObject.SetActive(true);
		}

		public void Hide()
		{
			gameObject.SetActive(false);
		}
	}
}