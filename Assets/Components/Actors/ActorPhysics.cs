using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MovementBase))]
public class ActorPhysics : MonoBehaviour
{
				[SerializeField]
				private float acceleration = 6.0f;
				[SerializeField]
				private float airAcceleration = 6.0f;

				[SerializeField]
				private float jumpSpeed = 8.0f;
				[SerializeField]
				private float gravity = 20.0f;

				[SerializeField]
				private float dragGround = 1;
				[SerializeField]
				private float dragAir = 1;

				[SerializeField]
				private Transform ground = null;

				private Rigidbody body;
				private MovementBase movement;
				private float lastJumpTime = 0;
				private const float jumpDelay = 0.25f;

				private void Awake()
				{
								body = GetComponent<Rigidbody>();
								movement = GetComponent<MovementBase>();
				}

				private bool IsGrounded
				{
								get
								{
												return ground ? Physics.Raycast(new Ray(ground.transform.position, Vector3.down), 0.2f) : false;
								}
				}

				private void FixedUpdate()
				{
								Vector3 deltaVelocity = Vector3.zero;
								Vector3 input = movement.Movement;

								float speed = body.velocity.magnitude;
								Vector3 direction = Vector3.zero;
								if (speed != 0)
								{
												direction = body.velocity / speed;
								}

								if (IsGrounded)
								{
												//Add drag on ground (linear)
												deltaVelocity -= direction * speed * dragGround * Time.deltaTime;

												deltaVelocity += input * acceleration * Time.deltaTime;

												if (movement.IsJumping && Time.time - lastJumpTime > jumpDelay)
												{
																deltaVelocity.y = jumpSpeed;
																lastJumpTime = Time.time;
												}
								}
								else
								{
												//Add drag in the air (quadradic)
												deltaVelocity -= direction * speed * speed * dragAir * Time.deltaTime;

												deltaVelocity += input * airAcceleration * Time.deltaTime;
								}

								deltaVelocity.y -= gravity * Time.deltaTime;

								body.AddForce(deltaVelocity, ForceMode.VelocityChange);
				}

				private void OnDisable()
				{
								//When disabling the actor physics on this gameObject, we want to gameObject to stand still
								body.AddForce(-body.velocity, ForceMode.VelocityChange);
				}
}