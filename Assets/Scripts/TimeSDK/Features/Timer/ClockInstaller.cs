using TimeSDK.Features.Clock.Implementation;
using TimeSDK.Features.Timer.Implementation;
using Zenject;

namespace TimeSDK.Features.Timer
{
	public class TimerInstaller : Installer<TimerInstaller>
	{
		public override void InstallBindings()
		{
			Container.BindInterfacesTo<TimerModel>().AsSingle();
			Container.BindInterfacesTo<TimerPresenter>().AsSingle();
		}
	}
}