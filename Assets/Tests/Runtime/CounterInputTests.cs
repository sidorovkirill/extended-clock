using System.Collections;
using System.Reflection;
using NUnit.Framework;
using TimeSDK.Features.Timer.Utils;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using Assert = UnityEngine.Assertions.Assert;

namespace Tests.Runtime
{
	public class CounterInputTests
	{
		private const string PrefabPath = "CounterInput";
		private const string ParametersFieldName = "_parameters";
		private const string UpButtonFieldName = "UpButton";
		private const string DownButtonFieldName = "DownButton";
		private const string ButtonsContainerName = "Buttons";
		
		private GameObject _counterObject;
		private CounterInput _script;
		
		[UnitySetUp]
		public IEnumerator SetUp()
		{
			var parameters = CreateParameters(0, 0, 3);

			var counterInputPrefab = Resources.Load<GameObject>(PrefabPath);
			_counterObject = MonoBehaviour.Instantiate(counterInputPrefab);
			_script = _counterObject.GetComponent<CounterInput>();
			TestUtils.SetToPrivateField(ParametersFieldName, _script, parameters);
			
			yield return null;
		}
		
		[UnityTest]
		public IEnumerator AddValueByClick()
		{
			var upButton = FindActionButton(UpButtonFieldName);
			
			yield return new WaitForSeconds(0.1f);
			
			upButton.onClick?.Invoke();

			var expectedResult = 1;
			var result = _script.Count.Value;
			
			Assert.AreEqual(expectedResult, result);
			
			yield return null;
		}
		
		[UnityTest]
		public IEnumerator TryToOverflowMaxValue()
		{
			var upButton = FindActionButton(UpButtonFieldName);
			
			yield return new WaitForSeconds(0.1f);

			for (int i = 0; i < 4; i++)
			{
				upButton.onClick?.Invoke();
			}

			var expectedResult = 3;
			var result = _script.Count.Value;
			
			Assert.AreEqual(expectedResult, result);
			
			yield return null;
		}
		
		[UnityTest]
		public IEnumerator TryToSetLessThanZero()
		{
			var downButton = FindActionButton(DownButtonFieldName);
			
			yield return new WaitForSeconds(0.1f);
			
			downButton.onClick?.Invoke();

			var expectedResult = 0;
			var result = _script.Count.Value;
			
			Assert.AreEqual(expectedResult, result);
			
			yield return null;
		}

		private Button FindActionButton(string name)
		{
			var buttons = _counterObject.transform.Find(ButtonsContainerName);
			Assert.IsNotNull(buttons);
			var upButton = buttons.transform.Find(name);
			Assert.IsNotNull(upButton);
			return upButton.GetComponent<Button>();
		}

		private static CounterParametersSO CreateParameters(int initialValue = 0, int minValue = 0, int maxValue = 0)
		{
			var parameters = ScriptableObject.CreateInstance<CounterParametersSO>();
			parameters.InitialValue = initialValue;
			parameters.MaxValue = maxValue;
			parameters.MinValue = minValue;

			return parameters;
		}
		
		[UnityTearDown]
		public IEnumerator TearDown()
		{
			MonoBehaviour.Destroy(_counterObject);
			yield return null;
		}
	}
}