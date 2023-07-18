using System;
using TimeSDK.Features.Clock.Infrastructure;
using TimeSDK.Features.Stopwatch.Infrastructure;
using TimeSDK.Features.Timer.Infrastructure;
using TimeSDK.Features.ToolBar.Infrastructure;
using TimeSDK.Features.ToolBar.Views;
using UniRx;
using Zenject;

namespace TimeSDK.Features.ToolBar.Implementation
{
	public class ToolBarModel : IToolBarModel, IInitializable, IDisposable
	{
		public event EventHandler OnTimerStart;
		public event EventHandler OnTimerFinish;
		public event EventHandler OnStopwatchStart;
		public event EventHandler OnStopwatchFinish;
		public ReactiveProperty<string> CurrentClockTime { get; private set; }
		public ReactiveProperty<long> CurrentStopwatchTime { get; private set; }
		public ReactiveProperty<long> CurrentTimerTime { get; private set; }
		public ToolBarItemName CurrentItem { get; private set; }

		private readonly IClockModel _clockModel;
		private readonly IStopwatchModel _stopwatchModel;
		private readonly ITimerModel _timerModel;

		public ToolBarModel(IClockModel clockModel, IStopwatchModel stopwatchModel, ITimerModel timerModel)
		{
			_clockModel = clockModel;
			_stopwatchModel = stopwatchModel;
			_timerModel = timerModel;

			CurrentClockTime = _clockModel.CurrentTime;
			CurrentStopwatchTime = _stopwatchModel.CurrentTime;
			CurrentTimerTime = _timerModel.CurrentTimeout;
		}

		public void Initialize()
		{
			SubscribeToEvents();
		}

		public void SetCurrentItem(ToolBarItemName currentItem)
		{
			CurrentItem = currentItem;
		}

		private void SubscribeToEvents()
		{
			_timerModel.OnFinish += TimerModelOnFinish;
			_timerModel.OnStart += TimerModelOnStart;
			_stopwatchModel.OnFinish += StopwatchModelOnFinish;
			_stopwatchModel.OnStart += StopwatchModelOnStart;
		}

		private void StopwatchModelOnStart(object sender, EventArgs e)
		{
			OnStopwatchStart?.Invoke(sender, e);
		}

		private void StopwatchModelOnFinish(object sender, EventArgs e)
		{
			OnStopwatchFinish?.Invoke(sender, e);
		}

		private void TimerModelOnStart(object sender, EventArgs e)
		{
			OnTimerStart?.Invoke(sender, e);
		}

		private void TimerModelOnFinish(object sender, EventArgs e)
		{
			OnTimerFinish?.Invoke(sender, e);
		}

		private void UnsubscribeFromEvents()
		{
			_timerModel.OnFinish -= TimerModelOnFinish;
			_timerModel.OnStart -= TimerModelOnStart;
			_stopwatchModel.OnFinish -= StopwatchModelOnFinish;
			_stopwatchModel.OnStart -= StopwatchModelOnStart;
		}

		public void Dispose()
		{
			UnsubscribeFromEvents();
		}
	}
}