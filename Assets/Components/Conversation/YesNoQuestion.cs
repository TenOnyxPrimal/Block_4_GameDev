using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[RequireComponent(typeof(UtteranceBase))]
public class YesNoQuestion : MonoBehaviour
{
				[SerializeField]
				[Multiline]
				private string utterance;

				[SerializeField]
				private UtteranceBase yes;

				[SerializeField]
				private UtteranceBase no;

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
								return new List<string>() { "yes", "no" };
				}

				private bool NextUtterance(int response, out UtteranceBase nextUtterance, out bool endConversation)
				{
								if (response == 0)
								{
												nextUtterance = yes;
												endConversation = endsConversation;
												return true;
								}
								else
								{
												if (response == 1)
												{
																nextUtterance = no;
																endConversation = endsConversation;
																return true;
												}
								}
								nextUtterance = utteranceBase;
								endConversation = false;

								return false;
				}
}
