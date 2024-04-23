using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InventoryBase : MonoBehaviour
{
				private readonly Dictionary<LayerMask, List<PickUpBase>> items = new Dictionary<LayerMask, List<PickUpBase>>();

				[SerializeField]
				private LayerMask pickUpLayerMask;

				[SerializeField]
				private List<PickUpBase> pickUpOnStartUp;

				public UnityAction onPickUp;

				private IEnumerator Start()
				{
								//In the first frame the event for pickups need to be setup
								//So we wait one frame, to force this pickup
								yield return null;

								foreach (PickUpBase pickUp in pickUpOnStartUp)
								{
												int layer = 1 << pickUp.gameObject.layer;
												PickUp(pickUp, layer);
								}
				}

				/*public void PickUp(LayerMask layer, PickUpBase pickUp)
				{
					if (!items.ContainsKey(layer))
						items.Add(layer, new List<PickUpBase>());

					items[layer].Add(pickUp);
					onPickUp?.Invoke();
				}*/

				private void OnTriggerEnter(Collider other)
				{
								int layer = 1 << other.gameObject.layer;

								if (pickUpLayerMask == (pickUpLayerMask | layer))
								{
												PickUpBase pickUp = other.gameObject.GetComponent<PickUpBase>();

												if (pickUp && pickUp.CanPickUp(1 << gameObject.layer))
												{
																PickUp(pickUp, layer);
												}
								}
				}

				private void PickUp(PickUpBase pickUp, int layer)
				{
								bool destroyAfterPickUp = pickUp.DestroyOnPickUp();

								pickUp.PickUpTarget = gameObject;
								pickUp.onPickUp?.Invoke();

								if (!destroyAfterPickUp)
								{
												//PickUp(layer, pickUp);
												if (!items.ContainsKey(layer))
																items.Add(layer, new List<PickUpBase>());

												items[layer].Add(pickUp);
												onPickUp?.Invoke();
								}
				}

				public List<PickUpBase> GetList(LayerMask type)
				{
								return items[type];
				}
}
