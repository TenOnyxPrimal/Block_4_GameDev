using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PickUpBase), typeof(WeaponBase))]
public class WeaponPickup : MonoBehaviour
{
				private PickUpBase pickUpBase;
				[SerializeField]
				private Collider pickUpTrigger;
				[SerializeField]
				private Vector3 socketOffset;

				// Start is called before the first frame update
				private void Start()
				{
								pickUpBase = GetComponent<PickUpBase>();
								pickUpBase.onPickUp += PickUpWeapon;
				}

				private Transform FindRecursively(Transform target, string name)
				{
								Transform newTarget = target.transform.Find("Socket");

								if (newTarget)
												return newTarget;

								for (int i = 0; i < target.childCount; i++)
								{
												// Call same function to search, except with the children of the object
												newTarget = FindRecursively(target.GetChild(i), name);

												if (!newTarget)
																continue;

												return newTarget;
								}

								return null;
				}

				// Update is called once per frame
				private void PickUpWeapon()
				{
								GameObject go = pickUpBase.PickUpTarget;

								Transform socket = FindRecursively(go.transform, "Socket");

								// Object should have a socket (like the player) where the weapon goes
								if (!socket)
												return;

								Transform weapon = transform;
								weapon.position = socket.position + socketOffset;
								weapon.parent = socket;
								weapon.rotation = socket.rotation;

								if (pickUpTrigger)
												Destroy(pickUpTrigger);

								// Destroy the weapon script so it functions as a regular weapon
								Destroy(this);
								Destroy(pickUpBase);
				}
}
