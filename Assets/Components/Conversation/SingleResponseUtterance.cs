using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[RequireComponent(typeof(UtteranceBase))]
public class SingleResponseUtterance : MonoBehaviour
{
				[SerializeField]
				[Multiline]
				private string utterance;

				[SerializeField]
				private string response;

				[SerializeField]
				private UtteranceBase next;

				[SerializeField]
				private bool endsConversation = false;

				private UtteranceBase utteranceBase;
				private void Awake()
				{
								utteranceBase = GetComponent<UtteranceBase>();
								utteranceBase.InitializeUtterance(GetText, GetResponse, NextUtterance);
				}

				private string GetText()
				{
								return utterance;
				}

				private List<string> GetResponse()
				{
								return new List<string>() { response };
				}

				private bool NextUtterance(int response, out UtteranceBase nextUtterance, out bool endConversation)
				{
								if (response == 0)
								{
												nextUtterance = next;
												endConversation = endsConversation;
												return true;
								}
								nextUtterance = utteranceBase;
								endConversation = false;
								return false;
				}
}
