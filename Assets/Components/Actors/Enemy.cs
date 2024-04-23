using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StateMachine))]
[RequireComponent(typeof(ActorAttack))]
public class Enemy : MonoBehaviour
{
				[SerializeField]
				private float scanAngle = 45f;

				[SerializeField]
				private State followingState;

				[SerializeField]
				private State firingState;

				[SerializeField]
				private TriggerEvent triggerEvent;

				private StateMachine stateMachine;
				private ActorAttack actorAttack;
				private Collider player;

				private void Start()
				{
								triggerEvent.onTriggerEnter += OnPlayerEnter;
								triggerEvent.onTriggerExit += OnPlayerExit;

								stateMachine = GetComponent<StateMachine>();
								actorAttack = GetComponent<ActorAttack>();
				}

				private void OnPlayerEnter(Collider player)
				{
								this.player = player;
								actorAttack.SetTarget(player);
				}

				private void OnPlayerExit(Collider player)
				{
								if (this.player == player)
												this.player = null;
				}

				private void Update()
				{
								if (!player)
												return;

								Vector3 toPlayer = player.transform.position - transform.position;
								float angleBetween = Vector3.Angle(toPlayer, transform.forward);
								if (angleBetween < scanAngle)
								{
												if (Physics.Raycast(transform.position, toPlayer, out RaycastHit hit))
												{
																if (hit.collider.gameObject == player.gameObject)
																{
																				stateMachine.SwitchState(firingState);
																				return;
																}
												}
								}
								stateMachine.SwitchState(followingState);
				}
}
