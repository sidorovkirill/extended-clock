using System;
using UniRx;

namespace TimeSDK.Features.Timer.Infrastructure
{
	public interface ITimerModel
	{
		event EventHandler OnStart;
		event EventHandler OnFinish;

		ReactiveProperty<long> CurrentTimeout { get; }

		void SetInitialValues(long initialTime, long delay);
		void UpdateTimeout(long newTime);
		void StopCounting();
	}
}