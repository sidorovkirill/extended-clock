using TimeSDK.Features.ToolBar.Views;

namespace TimeSDK.Features.ToolBar.Infrastructure
{
	public interface IToolBarView
	{
		ToolBarItem[] Items { get; }

		void ToggleViewByName(ToolBarItemName name, bool isHided);
		void ToggleClockFaceInItem(ToolBarItemName name, bool isHided);
		void UpdateClockFaceInItem(ToolBarItemName name, string time);
	}
}