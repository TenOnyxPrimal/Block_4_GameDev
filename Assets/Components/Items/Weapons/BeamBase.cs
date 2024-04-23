using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(ProjectileBase))]
public class BeamBase : MonoBehaviour
{
				private ProjectileBase projectileBase;
				[SerializeField]
				private float range;
				[SerializeField]
				private LayerMask beamLayerMask;

				public UnityAction onBeamHit;

				// Start is called before the first frame update
				void Awake()
				{
								projectileBase = GetComponent<ProjectileBase>();
								projectileBase.onShoot += Beam;
								onBeamHit += BeamHit;
				}

				// Update is called once per frame
				private void Beam()
				{
								if (Physics.Raycast(
										transform.position, transform.forward, out RaycastHit hit, range, beamLayerMask))
								{
												Debug.Log(hit.collider.gameObject);
												// Do something with Hit
												// I.e. add a line rendere component in start
												// Stretch the line renderer from start to end
								}
				}

				private void BeamHit()
				{
								projectileBase.onHit?.Invoke();
								onBeamHit?.Invoke();
				}
}
