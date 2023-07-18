using NUnit.Framework;
using TimeSDK.Utils;

namespace Tests.Editor
{
	public class TimeUtilsTests
	{
		private const int Milliseconds = 1000;
		private const int Seconds = 60;
		private const int Minutes = 60;
		
		[Test]
		public void TestFormatTimeWithoutHours()
		{
			var expectedResult = "03:00:00";
			var time = 3 * Seconds * Milliseconds;
			var result = TimeUtils.FormatTime(time);

			Assert.AreEqual(expectedResult, result);
		}
		
		[Test]
		public void TestFormatTime()
		{
			var expectedResult = "03:00:00:00";
			var time = 3 * Minutes * Seconds * Milliseconds;
			var result = TimeUtils.FormatTime(time);

			Assert.AreEqual(expectedResult, result);
		}
	}
}