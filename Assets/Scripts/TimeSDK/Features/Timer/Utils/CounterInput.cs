using System;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace TimeSDK.Features.Timer.Utils
{
	public class CounterInput : MonoBehaviour
	{
		public ReactiveProperty<int> Count;

		[SerializeField] private TMP_Text _title;
		[SerializeField] private TMP_Text _counter;
		[SerializeField] private Button _upArrowButton;
		[SerializeField] private Button _downArrowButton;
		[SerializeField] private CounterParametersSO _parameters;

		private IDisposable _upArrowButtonSubs;
		private IDisposable _downArrowButtonSubs;

		public void ResetInput()
		{
			Count.Value = _parameters.InitialValue;
			UpdateCounter();
		}

		private void Start()
		{
			Count = new ReactiveProperty<int>(_parameters.InitialValue);
			SetupTitle();
			MakeSubscriptions();
			UpdateCounter();
		}

		private void SetupTitle()
		{
			_title.text = _parameters.TimeUnit;
		}
		
		private void MakeSubscriptions()
		{
			_upArrowButtonSubs = _upArrowButton.OnClickAsObservable().Subscribe(AddValue);
			_downArrowButtonSubs = _downArrowButton.OnClickAsObservable().Subscribe(SubtractValue);
		}

		private void AddValue(Unit unit)
		{
			if (_parameters.MaxValue == 0 || Count.Value < _parameters.MaxValue)
			{
				Count.Value += 1;
			}

			UpdateCounter();
		}

		private void SubtractValue(Unit unit)
		{
			if (Count.Value > _parameters.MinValue)
			{
				Count.Value -= 1;
			}

			UpdateCounter();
		}

		private void UpdateCounter()
		{
			_counter.text = Count.Value.ToString();
		}

		private void DisposeSubscriptions()
		{
			_upArrowButtonSubs.Dispose();
			_downArrowButtonSubs.Dispose();
		}

		private void OnDestroy()
		{
			DisposeSubscriptions();
		}
	}
}