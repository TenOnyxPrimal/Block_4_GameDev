using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(PickUpBase))]
public class AmmoPickUp : MonoBehaviour
{
				private PickUpBase pickUpBase;
				[SerializeField]
				private int ammo;
				[SerializeField]
				private WeaponType ammoType;

				private void Awake()
				{
								pickUpBase = GetComponent<PickUpBase>();
								pickUpBase.onPickUp = PickUp;
				}

				private void PickUp()
				{
								if (ammo == 0 || ammoType == WeaponType.None)
												return;

								pickUpBase.PickUpTarget.GetComponent<WeaponManager>().AddAmmo(ammoType, ammo);
				}
}
