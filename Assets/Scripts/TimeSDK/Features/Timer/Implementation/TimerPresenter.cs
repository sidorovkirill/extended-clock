using System;
using TimeSDK.Features.Timer.Infrastructure;
using TimeSDK.Utils;
using UniRx;
using Zenject;

namespace TimeSDK.Features.Timer.Implementation
{
	public class TimerPresenter : ITimerPresenter, IInitializable, IDisposable
	{
		private const int UpdatePeriodInMilliseconds = 10;
		private readonly ITimerView _view;
		private readonly ITimerModel _model;
		private IDisposable _newTimeoutSubs;
		private IDisposable _checkForTimeEndSubs;
		private IDisposable _startButtonSubs;
		private IDisposable _playButtonSubs;
		private IDisposable _stopButtonSubs;
		private IDisposable _pauseButtonSubs;
		private IDisposable _restartButtonSubs;
		private IDisposable _intervalHandler;

		public TimerPresenter(ITimerModel model, ITimerView view)
		{
			_model = model;
			_view = view;
		}

		public void Initialize()
		{
			CreateSubscriptions();
		}

		public void Start(long timeInMilliseconds)
		{
			StartCountdown(timeInMilliseconds);
		}

		public void Pause()
		{
			HandlePause(new Unit());
		}

		public void Play()
		{
			HandlePlay(new Unit());
		}

		public void Stop()
		{
			HandleStop(new Unit());
		}

		public void Restart()
		{
			HandleStart(new Unit());
		}

		private void CreateSubscriptions()
		{
			_newTimeoutSubs = _model.CurrentTimeout.Subscribe(ShowNewTime);
			_checkForTimeEndSubs = _model.CurrentTimeout.Subscribe(CheckForTimeEnd);
			_startButtonSubs = _view.StartPanel.StartButton.OnClickAsObservable().Subscribe(HandleStart);
			_playButtonSubs = _view.TimerPanel.PlayButton.OnClickAsObservable().Subscribe(HandlePlay);
			_pauseButtonSubs = _view.TimerPanel.PauseButton.OnClickAsObservable().Subscribe(HandlePause);
			_stopButtonSubs = _view.TimerPanel.StopButton.OnClickAsObservable().Subscribe(HandleStop);
			_restartButtonSubs = _view.TimerPanel.RestartButton.OnClickAsObservable().Subscribe(HandleRestart);
		}

		private void ShowNewTime(long newTime)
		{
			_view.TimerPanel.Face.text = TimeUtils.FormatTime(newTime);
		}

		private void CheckForTimeEnd(long newTime)
		{
			if (newTime <= 0)
			{
				FinishCountdown();
				_view.PlayEndSound();
			}
		}

		private void HandleStart(Unit unit)
		{
			var timerIsStarted = true;
			_view.ToggleStartAndTimerPanels(timerIsStarted);
			var timerAmount = AssembleTimeout();
			StartCountdown(timerAmount);
		}

		private void HandlePause(Unit unit)
		{
			_intervalHandler.Dispose();
		}

		private void HandlePlay(Unit unit)
		{
			var startTime = TimeUtils.GetCurrentTimestamp();
			_model.SetInitialValues(startTime, _model.CurrentTimeout.Value);
			StartUpdateInterval();
		}

		private void HandleStop(Unit unit)
		{
			_intervalHandler.Dispose();
			var timerAmount = AssembleTimeout();
			StartCountdown(timerAmount);
		}

		private void HandleRestart(Unit unit)
		{
			FinishCountdown();
		}

		private void StartCountdown(long timerAmount)
		{
			var startTime = TimeUtils.GetCurrentTimestamp();
			_model.SetInitialValues(startTime, timerAmount);
			StartUpdateInterval();
		}

		private void FinishCountdown()
		{
			_intervalHandler.Dispose();
			ResetInputCounters();
			var timerIsStarted = false;
			_view.ToggleStartAndTimerPanels(timerIsStarted);
			_model.StopCounting();
		}

		private void StartUpdateInterval()
		{
			_intervalHandler = Observable.Interval(TimeSpan.FromMilliseconds(UpdatePeriodInMilliseconds))
				.Subscribe(UpdateTimeout);
		}

		private void UpdateTimeout(long newTime)
		{
			_model.UpdateTimeout(TimeUtils.GetCurrentTimestamp());
		}

		private long AssembleTimeout()
		{
			var hours = _view.StartPanel.HoursCounter.Count.Value;
			var minutes = _view.StartPanel.MinutesCounter.Count.Value;
			var seconds = _view.StartPanel.SecondsCounter.Count.Value;

			var timeSpan = new TimeSpan(hours, minutes, seconds);
			return (long)timeSpan.TotalMilliseconds;
		}

		private void ResetInputCounters()
		{
			_view.StartPanel.HoursCounter.ResetInput();
			_view.StartPanel.MinutesCounter.ResetInput();
			_view.StartPanel.SecondsCounter.ResetInput();
		}

		private void DisposeSubscriptions()
		{
			_intervalHandler?.Dispose();
			_newTimeoutSubs?.Dispose();
			_checkForTimeEndSubs?.Dispose();
			_startButtonSubs?.Dispose();
			_playButtonSubs?.Dispose();
			_pauseButtonSubs?.Dispose();
			_stopButtonSubs?.Dispose();
			_restartButtonSubs?.Dispose();
		}

		public void Dispose()
		{
			DisposeSubscriptions();
		}
	}
}