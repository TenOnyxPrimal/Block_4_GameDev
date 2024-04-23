using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorAttack : MonoBehaviour
{
				[SerializeField]
				private bool startAttacking = false;

				[SerializeField]
				private Collider defaultTarget;

				[SerializeField]
				private float rotateSpeed = 180;

				private bool isAttacking;
				private Collider target;

				private const float SMALL_ANGLE = 1f;

				private WeaponManager weaponManager;

				private void Awake()
				{
								isAttacking = startAttacking;
								target = defaultTarget;
								weaponManager = GetComponent<WeaponManager>();
				}

				public void SetTarget(Collider target)
				{
								this.target = target;
				}

				public void SetAttackState(bool isAttacking)
				{
								this.isAttacking = isAttacking;
				}

				private void Update()
				{
								if (target == null || !isAttacking)
												return;

								//Rotate to target
								Vector3 toTarget = target.transform.position - transform.position;
								Vector3 forward = transform.forward;
								toTarget.y = forward.y = 0;
								Vector3 direction = Vector3.RotateTowards(forward, toTarget, Mathf.Deg2Rad * rotateSpeed * Time.deltaTime, 0);
								transform.rotation = Quaternion.LookRotation(direction, Vector3.up);

								//Fire at target
								float angle = Vector3.Angle(direction, toTarget);
								if (angle <= SMALL_ANGLE && weaponManager)
								{
												//Debug.Log("Fire");

												weaponManager.Shoot();
								}
				}
}
