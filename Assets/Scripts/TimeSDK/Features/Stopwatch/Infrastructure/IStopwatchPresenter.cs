namespace TimeSDK.Features.Stopwatch.Infrastructure
{
	public interface IStopwatchPresenter
	{
		void Play();
		void Pause();
		void Restart();
		void FixLapTime();
	}
}