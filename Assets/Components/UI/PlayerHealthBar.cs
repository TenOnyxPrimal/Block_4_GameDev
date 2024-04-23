using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
				[SerializeField]
				private Image healthBar;

				private float previousFraction;
				private float currentFraction;
				private float currentTime;
				private bool updateHealthbar;

				private float health;
				private float maxHealth;

				private void Start()
				{
								HealthBase[] healthBases = FindObjectsOfType<HealthBase>();
								foreach (HealthBase healthBase in healthBases)
												if (healthBase.IsPlayer)
												{
																healthBase.onDamage += UpdateHealth;
																healthBase.onRegen += UpdateHealth;

																health = healthBase.CurrentHealth;
																maxHealth = healthBase.MaximumHealth;

																previousFraction = currentFraction = health / maxHealth;
																currentTime = 1;
																updateHealthbar = false;
												}
				}

				private void UpdateHealth(float health)
				{
								this.health = health;

								previousFraction = healthBar.fillAmount;
								currentFraction = health / maxHealth;
								currentTime = 0;

								updateHealthbar = true;
				}

				private void Update()
				{
								if (updateHealthbar)
								{
												currentTime += Time.deltaTime;

												if (currentTime > 1)
												{
																currentTime = 1;
																updateHealthbar = false;
												}

												healthBar.fillAmount = Mathf.Lerp(previousFraction, currentFraction, currentTime);
								}
				}
}
