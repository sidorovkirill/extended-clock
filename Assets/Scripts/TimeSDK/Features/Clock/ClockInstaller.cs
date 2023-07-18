using TimeSDK.Features.Clock.Implementation;
using Zenject;

namespace TimeSDK.Features.Clock
{
	public class ClockInstaller : Installer<ClockInstaller>
	{
		public override void InstallBindings()
		{
			Container.BindInterfacesTo<ClockModel>().AsSingle();
			Container.BindInterfacesTo<ClockPresenter>().AsSingle();
		}
	}
}