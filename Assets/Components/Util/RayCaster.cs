using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RayCaster : MonoBehaviour
{
				[SerializeField]
				private float distance = 1.0f;

				[SerializeField]
				private LayerMask layerMask;

				public UnityAction<RaycastHit, GameObject> newTarget;
				public UnityAction<GameObject> lostTarget;

				private GameObject target;

				private void OnDrawGizmos()
				{
								Debug.DrawRay(transform.position, transform.forward * distance, Color.red);
				}

				private void Update()
				{
								GameObject currentTarget;
								if (Physics.Raycast(new Ray(transform.position, transform.forward), out RaycastHit info, distance, layerMask.value))
								{
												currentTarget = info.collider.gameObject;
												if (currentTarget != target)
												{
																if (target)
																				lostTarget?.Invoke(target);
																target = currentTarget;
																newTarget?.Invoke(info, target);
												}
								}
								else
								{
												if (target)
																lostTarget?.Invoke(target);
												target = null;
								}
				}
}
