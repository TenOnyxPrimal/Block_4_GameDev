using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(MovementBase))]
public class ProjectileBase : MonoBehaviour
{
				private MovementBase movement;
				[SerializeField]
				private float speed;
				[SerializeField]
				private EffectBase hitEffectBase;
				[SerializeField]
				private float damage = 10;

				public UnityAction onShoot;
				public UnityAction onHit;

				private Vector3 impactNormal;
				private Vector3 impactPoint;

				HealthBase targetHealth;

				private void Awake()
				{
								movement = GetComponent<MovementBase>();
								onHit += Effect;
				}

				/// <summary>
				/// Update the movement
				/// TODO: Move ActorPhysics movement
				/// </summary>
				private void Update()
				{
								movement.AddMovement(transform.forward * speed);
				}

				/// <summary>
				/// Add the initial movement when shooting and invoke other shoot functionality
				/// I.e. you can have multiple projectiles in children bound to this onShoot (do it with += operator, line  23)
				/// </summary>
				public void Shoot()
				{
								movement.AddMovement(transform.forward * speed);

								onShoot?.Invoke();
				}

				/// <summary>
				/// Spawn the effect and invoke any other functions bound
				/// </summary>
				public void Effect()
				{
								if (!hitEffectBase)
												return;

								// Calculate the rotation
								Quaternion spawnRotation = Quaternion.LookRotation(impactNormal);

								// Other effects can use this rotation
								//EffectBase effect = Instantiate(hitEffectBase, impactPoint, spawnRotation);
								Instantiate(hitEffectBase, impactPoint, spawnRotation);

								// There's an issue with destroy, delegate is not called if destroyed immediately.
								Destroy(gameObject, Time.deltaTime);
				}

				private void OnCollisionEnter(Collision collision)
				{
								for (int i = 0; i < collision.contacts.Length; i++)
												impactPoint += collision.contacts[i].point;

								impactPoint /= collision.contacts.Length;
								impactNormal = collision.contacts[0].normal;

								targetHealth = collision.collider.GetComponent<HealthBase>();

								if (targetHealth)
												targetHealth.DoDamage(damage);

								onHit?.Invoke();
				}
}
