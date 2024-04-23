using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HealthBase : MonoBehaviour
{
				[SerializeField]
				private float health;
				private float maxHealth;
				[SerializeField]
				private bool canRegen;

				[SerializeField, Tooltip("Health per tick")]
				private float regenRate;
				[SerializeField, Tooltip("Ticks per second")]
				private float regenTickRate;
				private float regenFraction;
				private float regenLastTimestamp;
				[SerializeField]
				private bool destroyOnDeath;
				[SerializeField]
				private bool isPlayer;

				public UnityAction<float> onRegen;
				public UnityAction<float> onDamage;

				public bool IsPlayer => isPlayer;
				public float CurrentHealth => health;
				public float MaximumHealth => maxHealth;

				private void Awake()
				{
								regenFraction = 1 / regenTickRate;
								maxHealth = health;
				}

				private void Update()
				{
								if (CanRegen())
												Regen();
				}

				private bool CanRegen()
				{
								return canRegen && health < maxHealth && Time.time > regenLastTimestamp + regenFraction;
				}

				public void DoDamage(float damage)
				{
								health -= damage;
								if (health < 0)
												health = 0;

								onDamage?.Invoke(health);

								if (health <= 0 && destroyOnDeath)
								{
												if (!isPlayer)
												{
																Destroy(gameObject);
																return;
												}

												LoadSceneUI.GameOver();
								}
				}

				private void Regen()
				{
								regenLastTimestamp = Time.time;
								health += regenRate;

								if (health >= maxHealth)
												health = maxHealth;

								onRegen(health);
				}
}
