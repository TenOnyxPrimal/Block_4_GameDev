using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ConversationUI : MonoBehaviour
{
				[SerializeField]
				private TextMeshProUGUI utterance;

				[SerializeField]
				private Transform buttonGroup;

				[SerializeField]
				private Response responseButtonPrefab;

				[SerializeField]
				private string startConversationTrigger = "start";

				[SerializeField]
				private string endConversationTrigger = "end";

				private int startHash;
				private int endHash;

				private Animator animator;

				private ConversationManager conversation;

				private readonly List<Response> responses = new List<Response>();

				private void Awake()
				{
								animator = GetComponent<Animator>();

								startHash = Animator.StringToHash(startConversationTrigger);
								endHash = Animator.StringToHash(endConversationTrigger);
				}

				private void Start()
				{
								conversation = FindObjectOfType<ConversationManager>();

								conversation.onStartConversation += StartConversation;
								conversation.onResponse += UpdateConversation;
								conversation.onEndConversation += EndConversation;
				}

				private void OnResponse(int index)
				{
								conversation.Respond(index);
				}

				private void StartConversation(string utterance, List<string> responses)
				{
								animator.SetTrigger(startHash);
								UpdateConversation(utterance, responses);
				}

				private void UpdateConversation(string utterance, List<string> responses)
				{
								ActiveNumberOfResponses(responses.Count);
								for (int index = 0; index < responses.Count; ++index)
												this.responses[index].Initialize(index, responses[index]);

								this.utterance.text = utterance;
				}

				private void ActiveNumberOfResponses(int count)
				{
								//Create new buttons if there are more responses than buttons
								for (int index = responses.Count; index < count; ++index)
								{
												Response response = Instantiate<Response>(responseButtonPrefab, buttonGroup);
												response.onClick += OnResponse;
												responses.Add(response);
								}
								//Disable buttons if there are more buttons then responses
								for (int index = count; index < responses.Count; ++index)
												responses[index].gameObject.SetActive(false);
				}

				private void EndConversation()
				{
								animator.SetTrigger(endHash);
				}
}
