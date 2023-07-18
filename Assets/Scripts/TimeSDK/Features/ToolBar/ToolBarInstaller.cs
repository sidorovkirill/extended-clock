using TimeSDK.Features.ToolBar.Implementation;
using Zenject;

namespace TimeSDK.Features.ToolBar
{
	public class ToolBarInstaller : Installer<ToolBarInstaller>
	{
		public override void InstallBindings()
		{
			Container.BindInterfacesTo<ToolBarModel>().AsSingle();
			Container.BindInterfacesTo<ToolBarPresenter>().AsSingle();
		}
	}
}