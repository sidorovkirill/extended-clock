using System;
using TimeSDK.Features.ToolBar.Views;
using UniRx;

namespace TimeSDK.Features.ToolBar.Infrastructure
{
	public interface IToolBarModel
	{
		event EventHandler OnTimerStart;
		event EventHandler OnTimerFinish;
		event EventHandler OnStopwatchStart;
		event EventHandler OnStopwatchFinish;
		ReactiveProperty<string> CurrentClockTime { get; }
		ReactiveProperty<long> CurrentStopwatchTime { get; }
		ReactiveProperty<long> CurrentTimerTime { get; }
		ToolBarItemName CurrentItem { get; }

		void SetCurrentItem(ToolBarItemName currentItem);
	}
}