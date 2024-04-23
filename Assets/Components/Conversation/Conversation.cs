using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// TODO: Handle having the conversation a second time
/// </summary>
[RequireComponent(typeof(Animator))]
public class Conversation : MonoBehaviour
{
				[SerializeField]
				private UtteranceBase firstUtterance;

				[SerializeField]
				private string startConversationTrigger = "start";

				[SerializeField]
				private string endConversationTrigger = "end";

				private int startHash;
				private int endHash;

				private UtteranceBase currentUtterance;
				private Animator animator;

				/*public bool ConversationEnded
				{
					get
					{
						return currentUtterance == null;
					}
				}*/
				public bool HasLine => currentUtterance != null;

				private void Awake()
				{
								animator = GetComponent<Animator>();

								startHash = Animator.StringToHash(startConversationTrigger);
								endHash = Animator.StringToHash(endConversationTrigger);

								currentUtterance = firstUtterance;
				}

				public bool StartConversation(out string utterance, out List<string> responses)
				{
								if (currentUtterance == null)
								{
												utterance = "";
												responses = null;
												return false;
								}
								utterance = GetText();
								responses = GetResponses();

								animator.SetTrigger(startHash);
								return true;
				}

				public bool Respond(int response, out string utterance, out List<string> responses, out bool endConversation)
				{
								bool result = currentUtterance.NextUtterance(response, out currentUtterance, out endConversation);
								if (currentUtterance == null)
												endConversation = true;

								utterance = GetText();
								responses = GetResponses();

								if (endConversation)
												animator.SetTrigger(endHash);

								return result;
				}

				private string GetText()
				{
								return currentUtterance ? currentUtterance.GetText() : "";
				}

				private List<string> GetResponses()
				{
								return currentUtterance ? currentUtterance.GetResponses() : null;
				}
}
