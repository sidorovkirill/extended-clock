using TimeSDK.Features.Timer.Views;
using UnityEngine;

namespace TimeSDK.Features.Timer.Infrastructure
{
	public interface ITimerView
	{
		StartPanelView StartPanel { get; }
		TimerPanelView TimerPanel { get; }

		void ToggleStartAndTimerPanels(bool timerIsStarted);
		void PlayEndSound();
	}
}