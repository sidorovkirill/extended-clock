using TimeSDK.Features.Clock;
using TimeSDK.Features.Clock.Infrastructure;
using TimeSDK.Features.Stopwatch;
using TimeSDK.Features.Stopwatch.Infrastructure;
using TimeSDK.Features.Timer;
using TimeSDK.Features.Timer.Infrastructure;
using TimeSDK.Features.ToolBar;
using TimeSDK.Features.ToolBar.Infrastructure;
using TimeSDK.UI;
using UnityEngine;
using Zenject;

namespace TimeSDK
{
	public class GameInstaller : MonoInstaller
	{
		[SerializeField] private MainCanvasView _mainCanvasView;

		public override void InstallBindings()
		{
			ToolBarInstaller.Install(Container);
			ClockInstaller.Install(Container);
			StopwatchInstaller.Install(Container);
			TimerInstaller.Install(Container);

			InstallViews();
			InstallProvider();
		}

		private void InstallProvider()
		{
			Container.BindInterfacesTo<Provider.TimeSDK>().AsSingle();
		}

		private void InstallViews()
		{
			Container.Bind<IClockView>().FromInstance(_mainCanvasView.ClockView);
			Container.Bind<IStopwatchView>().FromInstance(_mainCanvasView.StopwatchView);
			Container.Bind<ITimerView>().FromInstance(_mainCanvasView.TimerView);
			Container.Bind<IToolBarView>().FromInstance(_mainCanvasView.ToolBarView);
		}
	}
}