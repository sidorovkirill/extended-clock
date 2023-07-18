using System.Reflection;
using TimeSDK.Features.Timer.Utils;

namespace Tests.Runtime
{
	public static class TestUtils
	{
		public static void SetToPrivateField<T>(string name, T instance, object value)
		{
			var privateAccessFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Default;
			typeof(T).GetField(name, privateAccessFlags)?.SetValue(instance, value);
		}
	}
}