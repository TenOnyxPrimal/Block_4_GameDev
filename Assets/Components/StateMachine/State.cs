using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class State : MonoBehaviour
{
				[SerializeField]
				private UnityEvent start;
				[SerializeField]
				private UnityEvent end;

				[SerializeField]
				private UnityEvent update;

				public void SwitchState(out Action start, out Action update, out Action end)
				{
								start = StartState;
								update = UpdateState;
								end = EndState;
				}

				private void StartState()
				{
								start?.Invoke();
				}

				private void UpdateState()
				{
								update?.Invoke();
				}

				private void EndState()
				{
								end?.Invoke();
				}
}
