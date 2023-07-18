using System;
using TimeSDK.Features.Timer.Infrastructure;
using TimeSDK.Features.Timer.Views;
using TimeSDK.UI;
using UniRx;
using UnityEngine;

namespace TimeSDK.Features.Timer.Implementation
{
	public class TimerView : ClosableViewPanel, ITimerView
	{
		[SerializeField] private StartPanelView _startPanel;
		[SerializeField] private TimerPanelView _timerPanel;
		[SerializeField] private AudioSource _timerEndSound;

		public StartPanelView StartPanel => _startPanel;
		public TimerPanelView TimerPanel => _timerPanel;

		public void ToggleStartAndTimerPanels(bool timerIsStarted)
		{
			if (timerIsStarted)
			{
				_startPanel.Hide();
				_timerPanel.Show();
			}
			else
			{
				_startPanel.Show();
				_timerPanel.Hide();
			}
		}

		public void PlayEndSound()
		{
			_timerEndSound.Play();
		}
	}
}