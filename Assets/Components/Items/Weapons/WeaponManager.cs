using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(InventoryBase))]
public class WeaponManager : MonoBehaviour
{
				private InventoryBase inventory;
				private readonly Dictionary<WeaponType, WeaponBase> weapons = new Dictionary<WeaponType, WeaponBase>();
				private readonly Dictionary<WeaponType, int> ammo = new Dictionary<WeaponType, int>();
				private WeaponType activeWeapon = WeaponType.None;

				[SerializeField, Tooltip("Only select one layer!")]
				private LayerMask weaponLayerMask;
				[SerializeField]
				private string mainFireAxis = "Fire1";
				[SerializeField]
				private string scrollAxis = "Mouse ScrollWheel";

				private UnityAction onShoot;
				private UnityAction onScroll;

				private void Awake()
				{
								// Make sure all ammo types exist
								for (int i = 0; i < Enum.GetValues(typeof(WeaponType)).Length; i++)
								{
												WeaponType type = (WeaponType)i;

												if (type != WeaponType.None)
																ammo.Add(type, 0);
								}

								onScroll += Scroll;
				}

				private void Start()
				{
								inventory = GetComponent<InventoryBase>();
								inventory.onPickUp += CheckForWeapons;
				}

				private void Update()
				{
								if (!string.IsNullOrWhiteSpace(mainFireAxis) && Input.GetButton(mainFireAxis))
												Shoot();

								if (Input.GetAxis(scrollAxis) != 0 && weapons.Count > 0)
												onScroll?.Invoke();
				}

				private void CheckForWeapons()
				{
								List<PickUpBase> newWeapons = inventory.GetList(weaponLayerMask);

								if (newWeapons.Count == weapons.Count)
												return;

								PickUpBase pickUpBase = newWeapons[newWeapons.Count - 1];

								WeaponBase weapon = pickUpBase.GetComponent<WeaponBase>();
								WeaponType slot = weapon.GetWeaponType();

								if (slot == WeaponType.None || weapons.ContainsKey(slot))
								{
												newWeapons.RemoveAt(newWeapons.Count - 1);
												int lootedAmmo = weapon.Unload();

												ammo[slot] += lootedAmmo;

												Destroy(weapon.gameObject);
												return;
								}

								weapons.Add(slot, weapon);
								weapon.Manager = this;

								if (activeWeapon == WeaponType.None)
												SetWeapon(slot);
								else
												weapon.gameObject.SetActive(false);
				}

				public void Shoot()
				{
								onShoot?.Invoke();
				}

				public int GetAmmo(WeaponType slot, int amount)
				{
								int reloadedAmmo = 0;

								// There are plenty of bullets left
								if (ammo[slot] - amount >= 0)
												reloadedAmmo = amount;
								// There are bullets left, but just not enough
								else if (ammo[slot] - amount > -amount)
												reloadedAmmo = ammo[slot];

								// It's okay if we do 0 - 0
								ammo[slot] -= reloadedAmmo;

								return reloadedAmmo;
				}

				public void AddAmmo(WeaponType slot, int amount)
				{
								ammo[slot] += amount;
				}

				private void SetWeapon(WeaponType slot)
				{
								if (activeWeapon != WeaponType.None)
												onShoot -= weapons[activeWeapon].HandleShoot;

								activeWeapon = slot;

								if (weapons.ContainsKey(slot))
								{
												weapons[slot].gameObject.SetActive(true);
												onShoot += weapons[slot].HandleShoot;
								}

								foreach (KeyValuePair<WeaponType, WeaponBase> kvp in weapons)
												if (kvp.Key != slot)
																kvp.Value.gameObject.SetActive(false);
				}

				private void Scroll()
				{
								float axis = Input.GetAxis(scrollAxis);

								if (axis > 0)
												ChangeWeapon(1);
								else
												ChangeWeapon(-1);
				}

				public void ChangeWeapon(int change)
				{
								int weapon = (int)activeWeapon;

								do
								{
												weapon += change;

												if (weapon >= Enum.GetValues(typeof(WeaponType)).Length)
																weapon = 0;
												else if (weapon < 0)
																weapon = Enum.GetValues(typeof(WeaponType)).Length - 1;
								}
								while (!weapons.ContainsKey((WeaponType)weapon));

								SwitchWeapon(weapon);
				}

				private void SwitchWeapon(int weapon)
				{
								if (activeWeapon != WeaponType.None)
								{
												weapons[activeWeapon].gameObject.SetActive(false);
												onShoot -= weapons[activeWeapon].HandleShoot;
								}

								activeWeapon = (WeaponType)weapon;

								if (activeWeapon != WeaponType.None)
								{
												weapons[activeWeapon].gameObject.SetActive(true);
												onShoot += weapons[activeWeapon].HandleShoot;
								}
				}
}
