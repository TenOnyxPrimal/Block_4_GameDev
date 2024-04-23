using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class TriggerEvent : MonoBehaviour
{
				[SerializeField]
				private LayerMask targetLayer;

				public UnityAction<Collider> onTriggerEnter;
				public UnityAction<Collider> onTriggerExit;

				private void OnTriggerEnter(Collider other)
				{
								if (LayerMaskContainsLayer(other.gameObject.layer))
								{
												onTriggerEnter?.Invoke(other);
								}
				}

				private void OnTriggerExit(Collider other)
				{
								if (LayerMaskContainsLayer(other.gameObject.layer))
								{
												onTriggerExit?.Invoke(other);
								}
				}

				private bool LayerMaskContainsLayer(int layer)
				{
								//Convert layer to layer mask format
								int mask = 1 << layer;
								//Compare if there are overlap in bit
								return (mask & targetLayer.value) != 0;
				}
}
