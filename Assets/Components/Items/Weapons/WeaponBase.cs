using UnityEngine;
using UnityEngine.Events;

public enum WeaponType
{
				None,
				Pistol,
				Rifle,
				Grenade
}

public class WeaponBase : MonoBehaviour
{
				[SerializeField]
				private Transform weaponMuzzle;
				private int currentAmmo;
				[SerializeField]
				private int maxAmmo;

				[SerializeField]
				private ProjectileBase projectilePrefab;
				[SerializeField, Tooltip("Only select one layer!")]
				private LayerMask projectileLayer;

				[SerializeField]
				private EffectBase fireEffect;
				private float lastFireTime;
				[SerializeField, Tooltip("Make sure it's longer than the effect time")]
				private float fireRate;
				private float fireFraction;

				private float lastReloadTime;
				[SerializeField]
				private float reloadTime = 1;

				[SerializeField]
				private WeaponType slot;
				private WeaponManager weaponManager;

				public UnityAction onShoot;

				public WeaponManager Manager { get => weaponManager; set => weaponManager = value; }

				private void Awake()
				{
								currentAmmo = maxAmmo == 0 ? 30 : maxAmmo;

								onShoot += SpawnProjectile;
								onShoot += SpawnEffect;

								fireFraction = 1 / fireRate;
				}

				/// <summary>
				/// This is the weapon that handles the firing mechanics
				/// </summary>
				public void HandleShoot()
				{
								// The fraction is a tiny optimization (prevent division)
								if (Time.time < lastFireTime + fireFraction
										|| Time.time < lastReloadTime + reloadTime)
												return;

								if (currentAmmo == 0)
								{
												// Get ammo from player (central storage)
												int ammo = Manager.GetAmmo(slot, maxAmmo);

												currentAmmo = ammo;

												// Wait until we're done with reloading
												if (currentAmmo > 0)
																lastReloadTime = Time.time;

												return;
								}

								// Reduce the current amount of ammo
								currentAmmo--;

								lastFireTime = Time.time;

								// Fire if something is bound
								onShoot?.Invoke();
				}

				private void SpawnProjectile()
				{
								if (!projectilePrefab)
												return;

								ProjectileBase projectile = Instantiate(projectilePrefab, weaponMuzzle.position, weaponMuzzle.rotation);
								projectile.gameObject.layer = (int)Mathf.Log(projectileLayer.value, 2);
				}

				private void SpawnEffect()
				{
								if (!fireEffect)
												return;

								EffectBase effect = Instantiate(fireEffect, weaponMuzzle.position, weaponMuzzle.rotation);
								effect.transform.parent = transform;
				}

				public WeaponType GetWeaponType()
				{
								return slot;
				}

				public int Unload()
				{
								int ammo = currentAmmo;
								currentAmmo = 0;
								return ammo;
				}
}
