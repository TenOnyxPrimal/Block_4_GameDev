using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class Response : MonoBehaviour
{
				[SerializeField]
				private TextMeshProUGUI response;

				public UnityAction<int> onClick;

				private int index = -1;

				private void Awake()
				{
								GetComponent<Button>().onClick.AddListener(OnClick);
				}

				private void OnClick()
				{
								onClick?.Invoke(index);
				}

				public void Initialize(int index, string response)
				{
								gameObject.SetActive(true);
								this.index = index;
								this.response.text = response;
				}
}
