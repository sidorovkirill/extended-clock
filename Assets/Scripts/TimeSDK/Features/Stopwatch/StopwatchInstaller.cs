using TimeSDK.Features.Stopwatch.Implementation;
using Zenject;

namespace TimeSDK.Features.Stopwatch
{
	public class StopwatchInstaller : Installer<StopwatchInstaller>
	{
		public override void InstallBindings()
		{
			Container.BindInterfacesTo<StopwatchModel>().AsSingle();
			Container.BindInterfacesTo<StopwatchPresenter>().AsSingle();
		}
	}
}