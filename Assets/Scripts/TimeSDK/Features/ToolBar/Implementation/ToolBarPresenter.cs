using System;
using TimeSDK.Features.ToolBar.Infrastructure;
using TimeSDK.Utils;
using UniRx;
using Zenject;

namespace TimeSDK.Features.ToolBar.Implementation
{
	public class ToolBarPresenter : IToolBarPresenter, IInitializable, IDisposable
	{
		private readonly IToolBarView _view;
		private readonly IToolBarModel _model;
		private IDisposable _clockUpdateSubs;
		private IDisposable _timerUpdateSubs;
		private IDisposable _stopwatchUpdateSubs;

		public ToolBarPresenter(IToolBarModel model, IToolBarView view)
		{
			_model = model;
			_view = view;
		}

		public void Initialize()
		{
			SubscribeToEvents();
		}

		private void HandleItemClick(object sender, ToolBarItemName name)
		{
			_view.ToggleViewByName(_model.CurrentItem, true);
			_model.SetCurrentItem(name);
			_view.ToggleViewByName(name, false);
		}

		private void SubscribeToEvents()
		{
			foreach (var item in _view.Items)
			{
				item.OnClick += HandleItemClick;
			}

			_model.OnTimerStart += ShowTimerFace;
			_model.OnTimerFinish += HideTimerFace;
			_model.OnStopwatchStart += ShowStopwatchFace;
			_model.OnStopwatchFinish += HideStopwatchFace;

			_clockUpdateSubs = _model.CurrentClockTime.Subscribe(UpdateClockFace);
			_stopwatchUpdateSubs = _model.CurrentStopwatchTime.Subscribe(UpdateStopwatchFace);
			_timerUpdateSubs = _model.CurrentTimerTime.Subscribe(UpdateTimerFace);
		}

		private void UpdateClockFace(string newTime)
		{
			_view.UpdateClockFaceInItem(ToolBarItemName.Clock, newTime);
		}

		private void UpdateStopwatchFace(long newTime)
		{
			var formatTime = TimeUtils.FormatTime(newTime);
			_view.UpdateClockFaceInItem(ToolBarItemName.Stopwatch, formatTime);
		}

		private void UpdateTimerFace(long newTime)
		{
			var formatTime = TimeUtils.FormatTime(newTime);
			_view.UpdateClockFaceInItem(ToolBarItemName.Timer, formatTime);
		}

		private void HideStopwatchFace(object sender, EventArgs e)
		{
			_view.ToggleClockFaceInItem(ToolBarItemName.Stopwatch, true);
		}

		private void ShowStopwatchFace(object sender, EventArgs e)
		{
			_view.ToggleClockFaceInItem(ToolBarItemName.Stopwatch, false);
		}

		private void HideTimerFace(object sender, EventArgs e)
		{
			_view.ToggleClockFaceInItem(ToolBarItemName.Timer, true);
		}

		private void ShowTimerFace(object sender, EventArgs e)
		{
			_view.ToggleClockFaceInItem(ToolBarItemName.Timer, false);
		}

		private void UnsubscribeFromEvents()
		{
			foreach (var item in _view.Items)
			{
				item.OnClick -= HandleItemClick;
			}

			_model.OnTimerStart -= ShowTimerFace;
			_model.OnTimerFinish -= HideTimerFace;
			_model.OnStopwatchStart -= ShowStopwatchFace;
			_model.OnStopwatchFinish -= HideStopwatchFace;

			_clockUpdateSubs?.Dispose();
			_stopwatchUpdateSubs?.Dispose();
			_timerUpdateSubs?.Dispose();
		}

		public void Dispose()
		{
			UnsubscribeFromEvents();
		}
	}
}