using TimeSDK.Features.Clock.Infrastructure;
using TimeSDK.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TimeSDK.Features.Clock.Implementation
{
	public class ClockView : ClosableViewPanel, IClockView
	{
		[SerializeField] private TMP_Text _face;

		public TMP_Text Face => _face;
	}
}