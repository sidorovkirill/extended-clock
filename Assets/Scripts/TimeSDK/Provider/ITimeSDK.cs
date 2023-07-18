using TimeSDK.Features.Stopwatch.Infrastructure;
using TimeSDK.Features.Timer.Infrastructure;

namespace TimeSDK.Provider
{
	public interface ITimeSDK
	{
		ITimerPresenter Timer { get; }
		IStopwatchPresenter Stopwatch { get; }
	}
}