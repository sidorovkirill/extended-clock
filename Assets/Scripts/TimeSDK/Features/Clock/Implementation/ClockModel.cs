using TimeSDK.Features.Clock.Infrastructure;
using TimeSDK.Utils;
using UniRx;

namespace TimeSDK.Features.Clock.Implementation
{
	public class ClockModel : IClockModel
	{
		public ReactiveProperty<string> CurrentTime { get; private set; }

		public ClockModel()
		{
			CurrentTime = new ReactiveProperty<string>(TimeUtils.GetCurrentTime());
		}
	}
}