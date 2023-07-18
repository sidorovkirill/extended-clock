using UnityEngine;

namespace TimeSDK.Features.Timer.Utils
{
	[CreateAssetMenu(fileName = "CounterParameter", menuName = "ScriptableObjects/CounterParameter", order = 1)]
	public class CounterParametersSO : ScriptableObject
	{
		public string TimeUnit;
		public int InitialValue = 0;
		public int MinValue = 0;
		public int MaxValue;
	}
}