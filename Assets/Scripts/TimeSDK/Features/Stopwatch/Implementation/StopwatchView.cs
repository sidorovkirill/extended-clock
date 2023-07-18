using System;
using TimeSDK.Features.Stopwatch.Infrastructure;
using TimeSDK.UI;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace TimeSDK.Features.Stopwatch.Implementation
{
	public class StopwatchView : ClosableViewPanel, IStopwatchView, IInitializable, IDisposable
	{
		public TMP_Text Face => _face;
		public TMP_Text LapTimeTitle => _lapTimeTitle;
		public Button PlayButton => _playButton;
		public Button PauseButton => _pauseButton;
		public Button LapTimeButton => _lapTimeButton;
		public Button RestartButton => _restartButton;

		[SerializeField] private TMP_Text _face;
		[SerializeField] private TMP_Text _lapTimeTitle;
		[SerializeField] private Button _pauseButton;
		[SerializeField] private Button _playButton;
		[SerializeField] private Button _lapTimeButton;
		[SerializeField] private Button _restartButton;
		private IDisposable _pauseButtonSubs;
		private IDisposable _playButtonSubs;

		public void Initialize()
		{
			_pauseButtonSubs = PauseButton.OnClickAsObservable().Subscribe(SetPlay);
			_playButtonSubs = PlayButton.OnClickAsObservable().Subscribe(SetPause);
		}

		public void SetPause(Unit unit)
		{
			TogglePlayAndPause(true);
		}

		public void SetPlay(Unit unit)
		{
			TogglePlayAndPause(false);
		}

		private void TogglePlayAndPause(bool isPaused)
		{
			PlayButton.gameObject.SetActive(!isPaused);
			PauseButton.gameObject.SetActive(isPaused);
		}

		public void Dispose()
		{
			_pauseButtonSubs?.Dispose();
			_playButtonSubs?.Dispose();
		}
	}
}