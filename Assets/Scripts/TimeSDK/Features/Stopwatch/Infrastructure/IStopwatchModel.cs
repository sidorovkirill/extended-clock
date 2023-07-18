using System;
using UniRx;

namespace TimeSDK.Features.Stopwatch.Infrastructure
{
	public interface IStopwatchModel
	{
		event EventHandler OnStart;
		event EventHandler OnFinish;

		ReactiveProperty<long> CurrentTime { get; }
		bool IsStarted { get; }

		void SetInitialTime(long initialTime);
		void UpdateTimeout(long newTime);
		void StopCounting();
	}
}