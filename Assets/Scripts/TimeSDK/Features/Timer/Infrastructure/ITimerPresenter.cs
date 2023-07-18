namespace TimeSDK.Features.Timer.Infrastructure
{
	public interface ITimerPresenter
	{
		void Start(long timeInMilliseconds);
		void Pause();
		void Play();
		void Stop();
		void Restart();
	}
}