using TimeSDK.Features.Stopwatch.Infrastructure;
using TimeSDK.Features.Timer.Infrastructure;

namespace TimeSDK.Provider
{
	public class TimeSDK : ITimeSDK
	{
		public ITimerPresenter Timer { get; }
		public IStopwatchPresenter Stopwatch { get; }

		public TimeSDK(ITimerPresenter timer, IStopwatchPresenter stopwatch)
		{
			Timer = timer;
			Stopwatch = stopwatch;
		}
	}
}