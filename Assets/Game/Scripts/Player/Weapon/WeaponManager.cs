using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
  [SerializeField] int secondaryAmmo;
  [SerializeField] int primaryAmmo;
  [SerializeField] List<SOWeapon> weaponInventory;
  [SerializeField] Camera fpsCam;
  [SerializeField] SOWeapon primarySlot1 = null;
  [SerializeField] SOWeapon primarySlot2 = null;
  [SerializeField] SOWeapon secondarySlot1 = null;
  [SerializeField] SOWeapon currentWeapon = null;


  private void Awake()
  {
    weaponInventory = new List<SOWeapon>();
    fpsCam = GetComponentInChildren<Camera>();

  }
  /// <summary>
  /// Adds the weapon to the Player's inventory on pickup. Slots the item in the appropriate slot if the slots are null.
  /// </summary>
  /// <param name="weapon">The weapon to add to the player's inventory</param>
  public void AddWeaponToInventory(SOWeapon weapon)
  {
    weaponInventory.Add(weapon);
    if (weapon.WeaponType == WeaponType.Primary)
    {
      EquipPrimary(weapon);
    }
    else if (weapon.WeaponType == WeaponType.Secondary && secondarySlot1 == null)
    {
      EquipSecondary(weapon);
    }

  }

  public bool EquipWeapon(SOWeapon weapon)
  {
    bool equipped = false;

    switch (weapon.WeaponType)
    {
      case WeaponType.Primary:
        equipped = EquipPrimary(weapon);

        break;
      case WeaponType.Secondary:
        EquipSecondary(weapon);
        equipped = true;
        break;
    }

    return equipped;
  }

  public void Attack()
  {
    Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
    RaycastHit hit;
    if (Physics.Raycast(ray, out hit))
    {
      print("Im shooting at " + hit.transform.name);
    }

  }

  bool EquipPrimary(SOWeapon weapon)
  {
    weaponInventory.Remove(weapon);
    if (primarySlot1 == null)
    {
      primarySlot1 = weapon;
      print("Equipped to slot 1");
      return true;
    }
    else if (primarySlot2 == null)
    {
      primarySlot2 = weapon;
      print("Equipped to slot 2");

      return true;
    }
    else
    {
      print("All slots full!");
      weaponInventory.Add(weapon);
      return false;
    }

  }
  void EquipSecondary(SOWeapon weapon)
  {
    // remove weapon to equip from inventory
    weaponInventory.Remove(weapon);

    if (secondarySlot1 != null)
    {
      // add what weapon is currently in slot
      weaponInventory.Add(secondarySlot1);
      // swap the weapon out
      secondarySlot1 = weapon;


    }
    else
    {
      // add it to secondary slot
      secondarySlot1 = weapon;
    }
  }

  public void UsePrimary()
  {
    if (primarySlot1 == null)
    {
      print("Nothing in primary slot!");
      return;
    }

    currentWeapon = primarySlot1;
    print(currentWeapon.name + " is equipped");

  }
  public void UseSecondary()
  {
    if (secondarySlot1 == null)
    {
      print("No secondary equipped!");
      return;
    }
    else
    {
      if (currentWeapon != null)
      {
        AddWeaponToInventory(currentWeapon);
        currentWeapon = null;
        UseSecondary();
      }
      currentWeapon = secondarySlot1;
      secondarySlot1 = null;
      print("Using " + currentWeapon.name);
    }
  }




}
