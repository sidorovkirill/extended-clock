using System;
using TimeSDK.Features.Clock.Infrastructure;
using TimeSDK.Features.Stopwatch.Infrastructure;
using TimeSDK.Features.Timer.Infrastructure;
using TimeSDK.Features.ToolBar.Infrastructure;
using TimeSDK.Features.ToolBar.Views;
using TimeSDK.UI;
using UnityEngine;
using Zenject;

namespace TimeSDK.Features.ToolBar.Implementation
{
	public class ToolBarView : ViewPanel, IToolBarView
	{
		public ToolBarItem[] Items => _items;

		[Inject] private IClockView _clockView;
		[Inject] private IStopwatchView _stopwatchView;
		[Inject] private ITimerView _timerView;
		[SerializeField] private ToolBarItem[] _items;


		public void ToggleViewByName(ToolBarItemName name, bool isHided)
		{
			var targetView = GetVewByName(name);
			if (isHided)
			{
				targetView.Hide();
			}
			else
			{
				targetView.Show();
			}
		}

		public void ToggleClockFaceInItem(ToolBarItemName name, bool isHided)
		{
			var targetItem = GetItemByName(name);
			targetItem.ToggleClockFace(isHided);
		}

		public void UpdateClockFaceInItem(ToolBarItemName name, string time)
		{
			var targetItem = GetItemByName(name);
			targetItem.ClockFace.text = time;
		}

		private ViewPanel GetVewByName(ToolBarItemName name)
		{
			return name switch
			{
				ToolBarItemName.Clock => (ViewPanel)_clockView,
				ToolBarItemName.Stopwatch => (ViewPanel)_stopwatchView,
				ToolBarItemName.Timer => (ViewPanel)_timerView,
				_ => throw new ArgumentOutOfRangeException(nameof(name), name, null)
			};
		}

		private ToolBarItem GetItemByName(ToolBarItemName name)
		{
			foreach (var item in _items)
			{
				if (item.Name == name)
				{
					return item;
				}
			}

			throw new Exception($"Item with name {name} doesn't exist");
		}
	}
}