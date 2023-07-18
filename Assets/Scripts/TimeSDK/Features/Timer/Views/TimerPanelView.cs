using System;
using TimeSDK.UI;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace TimeSDK.Features.Timer.Views
{
	public class TimerPanelView : ViewPanel
	{
		public TMP_Text Face => _face;
		public Button PlayButton => _playButton;
		public Button PauseButton => _pauseButton;
		public Button StopButton => _stopButton;
		public Button RestartButton => _restartButton;

		[SerializeField] private TMP_Text _face;
		[SerializeField] private Button _pauseButton;
		[SerializeField] private Button _playButton;
		[SerializeField] private Button _stopButton;
		[SerializeField] private Button _restartButton;
		private IDisposable _pauseButtonSubs;
		private IDisposable _playButtonSubs;
		private IDisposable _stopButtonSubs;
		private IDisposable _restartButtonSubs;

		private void Start()
		{
			_pauseButtonSubs = PauseButton.OnClickAsObservable().Subscribe(SetPlay);
			_playButtonSubs = PlayButton.OnClickAsObservable().Subscribe(SetPause);
			_stopButtonSubs = StopButton.OnClickAsObservable().Subscribe(SetStop);
			_restartButtonSubs = RestartButton.OnClickAsObservable().Subscribe(Restart);
		}

		public void SetPause(Unit unit)
		{
			TogglePlayAndPause(true);
		}

		public void SetPlay(Unit unit)
		{
			TogglePlayAndPause(false);
		}

		public void SetStop(Unit unit)
		{
			TogglePlayAndPause(true);
		}

		public void Restart(Unit unit)
		{
			TogglePlayAndPause(false);
		}

		private void TogglePlayAndPause(bool isPaused)
		{
			PlayButton.gameObject.SetActive(!isPaused);
			PauseButton.gameObject.SetActive(isPaused);
		}

		private void OnDestroy()
		{
			_pauseButtonSubs?.Dispose();
			_playButtonSubs?.Dispose();
			_stopButtonSubs?.Dispose();
			_restartButtonSubs?.Dispose();
		}
	}
}