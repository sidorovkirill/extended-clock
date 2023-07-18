using UniRx;

namespace TimeSDK.Features.Clock.Infrastructure
{
	public interface IClockModel
	{
		public ReactiveProperty<string> CurrentTime { get; }
	}
}