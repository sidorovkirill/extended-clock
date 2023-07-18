using TimeSDK.Features.Clock.Implementation;
using TimeSDK.Features.Stopwatch.Implementation;
using TimeSDK.Features.Timer.Implementation;
using TimeSDK.Features.ToolBar.Implementation;
using UnityEngine;

namespace TimeSDK.UI
{
	public class MainCanvasView : MonoBehaviour
	{
		public ClockView ClockView => _clockView;
		public StopwatchView StopwatchView => _stopwatchView;
		public TimerView TimerView => _timerView;
		public ToolBarView ToolBarView => _toolBarView;

		[SerializeField] private ClockView _clockView;
		[SerializeField] private StopwatchView _stopwatchView;
		[SerializeField] private TimerView _timerView;
		[SerializeField] private ToolBarView _toolBarView;
	}
}