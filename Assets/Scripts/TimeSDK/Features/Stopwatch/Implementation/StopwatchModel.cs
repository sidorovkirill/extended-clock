using System;
using TimeSDK.Features.Stopwatch.Infrastructure;
using TimeSDK.Utils;
using UniRx;

namespace TimeSDK.Features.Stopwatch.Implementation
{
	public class StopwatchModel : IStopwatchModel
	{
		public event EventHandler OnStart;
		public event EventHandler OnFinish;

		public ReactiveProperty<long> CurrentTime { get; private set; }
		public bool IsStarted { get; private set; } = false;
		private long _initialTime = 0;
		private long _spanBeforePause = 0;

		public StopwatchModel()
		{
			CurrentTime = new ReactiveProperty<long>(0);
		}

		public void SetInitialTime(long initialTime)
		{
			if (IsStarted)
			{
				_spanBeforePause = CurrentTime.Value;
			}
			else
			{
				IsStarted = true;
			}

			_initialTime = initialTime;
			OnStart?.Invoke(this, EventArgs.Empty);
		}

		public void UpdateTimeout(long newTime)
		{
			CurrentTime.Value = newTime - _initialTime + _spanBeforePause;
		}

		public void StopCounting()
		{
			IsStarted = false;
			_spanBeforePause = 0;
			CurrentTime.Value = 0;
			OnFinish?.Invoke(this, EventArgs.Empty);
		}
	}
}