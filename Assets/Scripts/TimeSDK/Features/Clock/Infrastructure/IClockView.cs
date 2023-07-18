using TimeSDK.UI;
using TMPro;
using UnityEngine.UI;

namespace TimeSDK.Features.Clock.Infrastructure
{
	public interface IClockView
	{
		TMP_Text Face { get; }
	}
}