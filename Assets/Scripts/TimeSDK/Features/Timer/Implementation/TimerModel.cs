using System;
using TimeSDK.Features.Timer.Infrastructure;
using UniRx;

namespace TimeSDK.Features.Timer.Implementation
{
	public class TimerModel : ITimerModel
	{
		public event EventHandler OnStart;
		public event EventHandler OnFinish;

		public ReactiveProperty<long> CurrentTimeout { get; private set; }
		private long _delay = 0;
		private long _initialTime = 0;

		public TimerModel()
		{
			CurrentTimeout = new ReactiveProperty<long>(1);
		}

		public void SetInitialValues(long initialTime, long delay)
		{
			OnStart?.Invoke(this, EventArgs.Empty);
			_initialTime = initialTime;
			_delay = delay;
		}

		public void UpdateTimeout(long newTime)
		{
			CurrentTimeout.Value = _initialTime - newTime + _delay;
		}

		public void StopCounting()
		{
			OnFinish?.Invoke(this, EventArgs.Empty);
		}
	}
}