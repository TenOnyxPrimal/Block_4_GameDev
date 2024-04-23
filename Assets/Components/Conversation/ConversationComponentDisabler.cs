using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationComponentDisabler : MonoBehaviour
{
				[SerializeField]
				private List<MonoBehaviour> disablingComponents;

				private ConversationManager manager;

				void Start()
				{
								manager = FindObjectOfType<ConversationManager>();
								manager.onStartConversation += StartConversation;
								manager.onEndConversation += EndConversation;

				}

				private void StartConversation(string line, List<string> responses)
				{
								foreach (var component in disablingComponents)
												component.enabled = false;
				}

				private void EndConversation()
				{
								foreach (var component in disablingComponents)
												component.enabled = true;
				}
}
