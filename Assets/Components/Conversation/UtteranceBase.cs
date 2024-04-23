using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

public class UtteranceBase : MonoBehaviour
{
				public delegate string GetTextDelegate();
				public delegate List<string> GetResponsesDelegate();
				public delegate bool NextUtteranceDelegate(int response, out UtteranceBase nextUtterance, out bool endConversation);

				private GetTextDelegate getText;
				private GetResponsesDelegate getResponses;
				private NextUtteranceDelegate nextUtterance;

				public void InitializeUtterance(GetTextDelegate getTextDelegate, GetResponsesDelegate responsesDelegate, NextUtteranceDelegate nextUtteranceDelegate)
				{
								getText = getTextDelegate;
								getResponses = responsesDelegate;
								nextUtterance = nextUtteranceDelegate;
				}

				public string GetText()
				{
								if (getText == null)
												return "";
								return getText();
				}

				public List<string> GetResponses()
				{
								if (getResponses == null)
												return new List<string>();
								return getResponses();
				}

				public bool NextUtterance(int response, out UtteranceBase nextUtterance, out bool endConversation)
				{
								if (this.nextUtterance == null)
								{
												nextUtterance = null;
												endConversation = true;
												return false;
								}
								return this.nextUtterance(response, out nextUtterance, out endConversation);
				}
}
