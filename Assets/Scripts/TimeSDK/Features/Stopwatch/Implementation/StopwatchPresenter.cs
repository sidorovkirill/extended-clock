using System;
using TimeSDK.Features.Clock.Infrastructure;
using TimeSDK.Features.Stopwatch.Infrastructure;
using TimeSDK.Features.Timer.Infrastructure;
using TimeSDK.Utils;
using UniRx;
using Zenject;

namespace TimeSDK.Features.Stopwatch.Implementation
{
	public class StopwatchPresenter : IStopwatchPresenter, IInitializable, IDisposable
	{
		private const string LapTimeLabel = @"Last lap time: {0}";
		private const int UpdatePeriodInMilliseconds = 10;
		private readonly IStopwatchView _view;
		private readonly IStopwatchModel _model;
		private IDisposable _newTimeoutSubs;
		private IDisposable _startButtonSubs;
		private IDisposable _playButtonSubs;
		private IDisposable _pauseButtonSubs;
		private IDisposable _lapTimeButtonSubs;
		private IDisposable _restartButtonSubs;
		private IDisposable _intervalHandler;

		public StopwatchPresenter(IStopwatchModel model, IStopwatchView view)
		{
			_model = model;
			_view = view;
		}

		public void Initialize()
		{
			CreateSubscriptions();
		}

		public void Play()
		{
			HandlePlay(new Unit());
		}

		public void Pause()
		{
			HandlePause(new Unit());
		}

		public void Restart()
		{
			HandleRestart(new Unit());
		}

		public void FixLapTime()
		{
			HandleShowLapTime(new Unit());
		}
		
		private void CreateSubscriptions()
		{
			_newTimeoutSubs = _model.CurrentTime.Subscribe(ShowNewTime);
			_playButtonSubs = _view.PlayButton.OnClickAsObservable().Subscribe(HandlePlay);
			_pauseButtonSubs = _view.PauseButton.OnClickAsObservable().Subscribe(HandlePause);
			_lapTimeButtonSubs = _view.LapTimeButton.OnClickAsObservable().Subscribe(HandleShowLapTime);
			_restartButtonSubs = _view.RestartButton.OnClickAsObservable().Subscribe(HandleRestart);
		}

		private void ShowNewTime(long newTime)
		{
			_view.Face.text = TimeUtils.FormatTime(newTime);
		}

		private void HandlePlay(Unit unit)
		{
			var startTime = TimeUtils.GetCurrentTimestamp();
			_model.SetInitialTime(startTime);
			StartUpdateInterval();
		}

		private void HandlePause(Unit unit)
		{
			_intervalHandler?.Dispose();
		}

		private void HandleRestart(Unit unit)
		{
			_intervalHandler?.Dispose();
			_model.StopCounting();
			_view.SetPlay(new Unit());
		}

		private void HandleShowLapTime(Unit unit)
		{
			var lapTime = TimeUtils.FormatTime(_model.CurrentTime.Value);
			_view.LapTimeTitle.text = String.Format(LapTimeLabel, lapTime);
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

		private void DisposeSubscriptions()
		{
			_intervalHandler?.Dispose();
			_newTimeoutSubs?.Dispose();
			_startButtonSubs?.Dispose();
			_playButtonSubs?.Dispose();
			_pauseButtonSubs?.Dispose();
			_lapTimeButtonSubs?.Dispose();
			_restartButtonSubs?.Dispose();
		}

		public void Dispose()
		{
			DisposeSubscriptions();
		}
	}
}