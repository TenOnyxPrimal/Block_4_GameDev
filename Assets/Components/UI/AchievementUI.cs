using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AchievementUI : MonoBehaviour
{
				[SerializeField]
				private TMPro.TextMeshProUGUI description;

				[SerializeField]
				private string showTrigger = "Show";

				private AchievementSystem system;
				private Animator animator;

				private int showHash;
				private void Start()
				{
								system = FindObjectOfType<AchievementSystem>();
								system.Achieved += AchievementAchieved;
								showHash = Animator.StringToHash(showTrigger);
								animator = GetComponent<Animator>();
				}

				private void AchievementAchieved(Achievement achievement)
				{
								description.text = achievement.Description;
								animator.SetTrigger(showHash);
				}
}
