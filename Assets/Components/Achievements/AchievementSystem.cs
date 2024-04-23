using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.Events;

public class AchievementSystem : MonoBehaviour
{
				[SerializeField]
				private List<Achievement> achievements;

				public ReadOnlyCollection<Achievement> Achievements => achievements.AsReadOnly();

				public event UnityAction<Achievement> Achieved;
				private void Awake()
				{
								foreach (Achievement achievement in achievements)
								{
												achievement.Reset();
												achievement.Achieved += this.AchievementAchieved;
								}
				}

				private void OnDestroy()
				{
								foreach (Achievement achievement in achievements)
								{
												achievement.Reset();
												achievement.Achieved -= this.AchievementAchieved;
								}
				}
				private void AchievementAchieved(Achievement achievement)
				{
								Achieved?.Invoke(achievement);
								Debug.Log(achievement.Description);
				}
}
