using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ConversationTargetState : MonoBehaviour
{
				public UnityEvent enableTarget;
				public UnityEvent disableTarget;

				private void Start()
				{
								//TODO: Find through game manager
								ConversationManager manager = FindObjectOfType<ConversationManager>();
								manager.updateTargetState += UpdateState;
				}

				private void UpdateState(bool state)
				{
								if (state)
												enableTarget?.Invoke();
								else
												disableTarget?.Invoke();
				}
}
