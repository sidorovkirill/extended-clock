using System;
using UnityEngine;

namespace TimeSDK.Utils
{
	public static class TimeUtils
	{
		private const string TimePattern = "{0:D2}:{1:D2}:{2:D2}:{3:D2}";
		private const string TimePatternWithoutHour = "{0:D2}:{1:D2}:{2:D2}";

		public static string FormatTime(long time)
		{
			var timespan = TimeSpan.FromMilliseconds(time);
			var milliseconds = timespan.Milliseconds / 10;

			if (timespan.Hours > 0)
			{
				return String.Format(TimePattern, timespan.Hours, timespan.Minutes, timespan.Seconds, milliseconds);
			}
			else
			{
				return String.Format(TimePatternWithoutHour, timespan.Minutes, timespan.Seconds, milliseconds);
			}
		}

		public static long GetCurrentTimestamp()
		{
			return Mathf.CeilToInt(Time.time * 1000);
		}

		public static string GetCurrentTime()
		{
			return DateTime.Now.ToString("HH:mm");
		}
	}
}