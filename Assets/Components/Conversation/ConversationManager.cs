using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//TODO: We can make this clearer by splitting the manager from the interaction (RayCaster)
public class ConversationManager : MonoBehaviour
{
				[SerializeField]
				private RayCaster conversationFinder;

				[SerializeField]
				private KeyCode startConversation;

				public UnityAction<string, List<string>> onStartConversation;
				public UnityAction<string, List<string>> onResponse;
				public UnityAction onEndConversation;

				public UnityAction<bool> updateTargetState;

				private Conversation conversation;
				private bool inConversation = false;
				private List<string> responses;

				private bool targetState = false;

				private void Start()
				{
								conversationFinder.newTarget += NewTarget;
								conversationFinder.lostTarget += LostTarget;

								onStartConversation += UpdateConversation;
								onResponse += UpdateConversation;
				}

				public void Respond(int index)
				{
								if (conversation.Respond(index, out string utterance, out responses, out bool endConversation))
								{
												inConversation = !endConversation;
												onResponse?.Invoke(utterance, responses);

												if (endConversation)
												{
																Debug.Log("End conversation");
																onEndConversation?.Invoke();
												}
								}

								UpdateTargetState();
				}

				private void NewTarget(RaycastHit hit, GameObject gameObject)
				{
								if (!inConversation)
												conversation = gameObject.GetComponent<Conversation>();

								UpdateTargetState();
				}

				private void LostTarget(GameObject gameObject)
				{
								if (!inConversation && conversation)
								{
												conversation = null;
								}
								UpdateTargetState();
				}

				private void UpdateTargetState()
				{
								bool currentState = conversation && conversation.HasLine && !inConversation;
								if (currentState != targetState)
								{
												targetState = currentState;
												updateTargetState?.Invoke(currentState);
								}
				}

				private void Update()
				{
								if (conversation != null && conversation.HasLine && Input.GetKeyDown(startConversation))
								{
												StartConversation();
								}

								if (inConversation)
								{
												for (int responseIndex = 0; responseIndex < responses.Count; ++responseIndex)
												{
																if (Input.GetKeyDown(KeyCode.Alpha1 + responseIndex))
																{
																				Respond(responseIndex);
																				break;
																}
												}
								}
				}

				private void StartConversation()
				{
								if (conversation.StartConversation(out string utterance, out responses))
								{
												inConversation = true;
												onStartConversation?.Invoke(utterance, responses);
								}

								UpdateTargetState();
				}

				private void UpdateConversation(string utterance, List<string> responses)
				{
								if (inConversation)
								{
												Debug.Log(utterance);
												foreach (string response in responses)
																Debug.Log(response);
								}
				}
}
