using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(ParticleSystem))]
public class EffectBase : MonoBehaviour
{
				private ParticleSystem particles;
				[HideInInspector]
				public Vector3 normal;

				[SerializeField]
				private float lifetime;
				// Disable this in editor if you want to pool the object
				[SerializeField]
				private bool destroyOnFinished = true;
				public UnityAction onEffect;
				public UnityAction onUpdate;

				public float Lifetime
				{
								get { return lifetime; }
				}

				[SerializeField]
				private bool hasParticles;
				private float startTimeStamp;

				private void Awake()
				{
								particles = GetComponent<ParticleSystem>();

								if (hasParticles)
												onEffect += Play;
				}

				/// <summary>
				/// Enable when this object begins to play
				/// </summary>
				private void Start()
				{
								StartEffect();
				}

				private void Update()
				{
								if (IsActive())
												onUpdate?.Invoke();

								if (!destroyOnFinished && !IsActive())
												gameObject.SetActive(false);
				}

				private void Play()
				{
								if (particles)
												particles.Play();
				}

				public void StartEffect()
				{
								if (destroyOnFinished)
												Destroy(gameObject, Lifetime);
								else
												gameObject.SetActive(true);

								startTimeStamp = Time.time;
								// Call other effects that might be bound
								onEffect?.Invoke();
				}

				/// <summary>
				/// This is for when pooling is implemented
				/// </summary>
				/// <returns>Whether this effect is active or not</returns>
				public bool IsActive()
				{
								return startTimeStamp + lifetime > Time.time;
				}
}
