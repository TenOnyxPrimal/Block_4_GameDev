using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StateMachine : MonoBehaviour
{
				[SerializeField]
				private State initialState;

				private Action updateState;
				private Action endState;

				private State currentState;
				private void Awake()
				{
								SwitchState(initialState);
				}

				public void SwitchState(State state)
				{
								if (state == currentState)
												return;

								//Debug.Log("New State: " + state.name);

								endState?.Invoke();
								currentState = state;
								currentState.SwitchState(out Action startState, out updateState, out endState);
								startState?.Invoke();
				}

				private void Update()
				{
								updateState?.Invoke();
				}
}
