using TMPro;
using UniRx;
using UnityEngine.UI;

namespace TimeSDK.Features.Stopwatch.Infrastructure
{
	public interface IStopwatchView
	{
		TMP_Text Face { get; }
		TMP_Text LapTimeTitle { get; }
		Button PlayButton { get; }
		Button PauseButton { get; }
		Button LapTimeButton { get; }
		Button RestartButton { get; }

		void SetPause(Unit unit);
		void SetPlay(Unit unit);
	}
}