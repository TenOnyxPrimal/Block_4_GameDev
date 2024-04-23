using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PickUpBase : MonoBehaviour
{
				[SerializeField]
				private LayerMask pickUpLayerMask;
				[SerializeField]
				private bool destroyOnPickup;
				[SerializeField]
				private float rotationAngle = 20f;

				private GameObject pickUpTarget;
				public UnityAction onPickUp;

				public GameObject PickUpTarget { get => pickUpTarget; set => pickUpTarget = value; }

				private void Start()
				{
								onPickUp += PickUp;
				}

				private void Update()
				{
								transform.Rotate(Vector3.up, rotationAngle * Time.deltaTime);
				}

				public bool CanPickUp(LayerMask layer)
				{
								return pickUpLayerMask == (pickUpLayerMask | layer);
				}

				private void PickUp()
				{
								if (destroyOnPickup)
												Destroy(gameObject);
				}

				public bool DestroyOnPickUp()
				{
								return destroyOnPickup;
				}
}
