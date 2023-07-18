using System;
using TimeSDK.Features.Clock.Infrastructure;
using TimeSDK.Utils;
using UniRx;
using Zenject;

namespace TimeSDK.Features.Clock.Implementation
{
	public class ClockPresenter : IClockPresenter, IInitializable, IDisposable
	{
		private const int UpdatePeriodInSeconds = 1;
		private readonly IClockModel _clockModel;
		private readonly IClockView _clockView;
		private IDisposable _intervalHandler;
		private IDisposable _updateClockSubs;

		public ClockPresenter(IClockModel clockModel, IClockView clockView)
		{
			_clockModel = clockModel;
			_clockView = clockView;
		}

		public void Initialize()
		{
			SubscribeOnEvents();
			StartUpdateInterval();
		}

		private void SubscribeOnEvents()
		{
			_updateClockSubs = _clockModel.CurrentTime.Subscribe(UpdateClockFace);
		}

		private void UpdateClockFace(string time)
		{
			_clockView.Face.text = time;
		}

		private void StartUpdateInterval()
		{
			_intervalHandler = Observable.Interval(TimeSpan.FromSeconds(UpdatePeriodInSeconds))
				.Subscribe(TimeChanged);
		}

		private void TimeChanged(long time)
		{
			_clockModel.CurrentTime.Value = TimeUtils.GetCurrentTime();
		}

		private void UnsubscribeFromEvents()
		{
			_intervalHandler?.Dispose();
			_updateClockSubs?.Dispose();
		}

		public void Dispose()
		{
			UnsubscribeFromEvents();
		}
	}
}