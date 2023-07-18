using TimeSDK.Features.Timer.Utils;
using TimeSDK.UI;
using UnityEngine;
using UnityEngine.UI;

namespace TimeSDK.Features.Timer.Views
{
	public class StartPanelView : ViewPanel
	{
		[SerializeField] private CounterInput _hoursCounter;
		[SerializeField] private CounterInput _minutesCounter;
		[SerializeField] private CounterInput _secondsCounter;
		[SerializeField] private Button _startButton;

		public CounterInput HoursCounter => _hoursCounter;
		public CounterInput MinutesCounter => _minutesCounter;
		public CounterInput SecondsCounter => _secondsCounter;
		public Button StartButton => _startButton;
	}
}