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
  SOWeapon primarySlot2 = null;
  [SerializeField] SOWeapon secondarySlot1 = null;
  SOWeapon currentWeapon = null;


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
    if (weapon.WeaponType == WeaponType.Primary && primarySlot1 == null)
    {
      EquipPrimary(weapon);
    }
    else if (weapon.WeaponType == WeaponType.Secondary && secondarySlot1 == null)
    {
      EquipSecondary(weapon);
    }
    print("The list has " + weaponInventory[0]);
    if (primarySlot1 == null) print("Primary is null");
    // print("Added weapon " + primarySlot1.name);
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
    if (primarySlot1 == null)
    {
      primarySlot1 = weapon;
      print("Equipped to slot 1");
      return true;
    }
    else if (primarySlot2 == null)
    {
      primarySlot2 = weapon;
      return true;
    }
    else
    {
      print("All slots full!");
      return false;
    }

  }

  void EquipSecondary(SOWeapon weapon)
  {
    SOWeapon currentWeapon = secondarySlot1;

    if (currentWeapon == null)
    {
      weaponInventory.Remove(weapon);
      secondarySlot1 = weapon;

    }
    else
    {
      weaponInventory.Add(currentWeapon);
      weaponInventory.Remove(weapon);
      secondarySlot1 = weapon;
      print("secondary slot full");

    }
  }


}
