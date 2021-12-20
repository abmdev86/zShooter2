using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
  [SerializeField] int secondaryAmmo;
  public int SecondaryAmmo
  {
    get
    {
      return secondaryAmmo;
    }
  }
  [SerializeField] int primaryAmmo;
  public int PrimaryAmmo
  {
    get
    {
      return primaryAmmo;
    }
  }

  [SerializeField] List<SOWeapon> weaponInventory;
  [SerializeField] Camera fpsCam;
  [SerializeField] SOWeapon primarySlot1 = null;
  [SerializeField] SOWeapon primarySlot2 = null;
  [SerializeField] SOWeapon secondarySlot1 = null;
  [SerializeField] SOWeapon currentWeapon = null;
  public SOWeapon CurrentWeapon
  {
    get
    {
      return currentWeapon;
    }
  }
  [SerializeField] GameObject weaponPlacementParent;
  [SerializeField] LayerMask ignoreLayer;
  GameObject currentWeaponModel = null;


  UIManager uIManager;

  private void Awake()
  {
    weaponInventory = new List<SOWeapon>();
    fpsCam = GetComponentInChildren<Camera>();
    uIManager = FindObjectOfType<UIManager>();

  }


  /// <summary>
  /// Adds the weapon to the Player's inventory on pickup. Slots the item in the appropriate slot if the slots are null.
  /// </summary>
  /// <param name="weapon">The weapon to add to the player's inventory</param>
  public void AddWeaponToInventory(SOWeapon weapon)
  {
    weapon.InitWeapon();
    // weaponInventory.Add(weapon);
    switch (weapon.WeaponType)
    {
      case WeaponType.Primary:
        if (primarySlot1 == null)
        {
          EquipPrimary(weapon);
        }
        else if (primarySlot2 = null)
        {
          EquipPrimary(weapon, 2);
        }
        else
        {
          weaponInventory.Add(weapon);
        }
        break;
      case WeaponType.Secondary:
        if (secondarySlot1 == null)
        {
          EquipSecondary(weapon);
        }
        else
        {
          weaponInventory.Add(weapon);
        }
        break;
    }


  }
  /// <summary>
  /// fires the weapon.
  /// </summary>
  public void Attack()
  {
    if (currentWeapon == null) return;
    if (currentWeapon.CurrentAmmo == 0)
    {
      ReloadWeapon();
    }

    Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
    RaycastHit hit;
    if (Physics.Raycast(ray, out hit, currentWeapon.Range, ignoreLayer))
    {
      if (hit.collider.gameObject.tag == "Hitbox")
      {
        ITargetable target = hit.collider.GetComponentInParent<ITargetable>();
        target.TakeDamage(currentWeapon.Damage, hit.point, hit.normal);
        Debug.DrawRay(ray.origin, ray.direction, Color.red);
      }
      print("Using" + currentWeapon.name + " on " + hit.transform.name);
    }

  }


  private void ReloadWeapon()
  {
    switch (currentWeapon.WeaponType)
    {
      case WeaponType.Primary:
        primaryAmmo -= currentWeapon.MaxAmmo;
        currentWeapon.Reload(primaryAmmo);
        break;
      case WeaponType.Secondary:
        currentWeapon.Reload(secondaryAmmo);
        break;
    }
  }

  /// <summary>
  /// Equip to an open Primary slot checking 1 and then 2 or adding back to list if none are open
  /// </summary>
  /// <param name="weapon"></param>
  /// <returns></returns>
  void EquipPrimary(SOWeapon weapon, int slotNumber = 1)
  {
    if (!CheckPrimarySlot(slotNumber)) return;
    if (weapon.WeaponType == WeaponType.Secondary) return;
    weaponInventory.Remove(weapon);
    switch (slotNumber)
    {
      case 1:
        primarySlot1 = weapon;

        break;
      case 2:
        primarySlot2 = weapon;
        break;
    }


  }
  /// <summary>
  /// Returns true if slot is empty.
  /// </summary>
  /// <param name="slot"> slot to check</param>
  /// <returns></returns>
  bool CheckPrimarySlot(int slot = 1)
  {
    switch (slot)
    {
      case 1:
        if (primarySlot1 != null)
        {
          return false;
        }
        else
        {
          return true;
        }
      case 2:
        if (primarySlot2 != null)
        {
          return false;
        }
        else
        {
          return true;
        }
      default:
        return false;
    }
  }


  /// <summary>
  /// equip to secondary slot
  /// </summary>
  /// <param name="weapon"></param>
  void EquipSecondary(SOWeapon weapon)
  {
    //check if its a primary just in case.
    if (weapon.WeaponType == WeaponType.Primary) return;
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

  public void UsePrimary(int slot = 1)
  {
    if (CheckPrimarySlot(slot)) return; // slot is empty so cant use it.

    if (currentWeapon != null) // There is a weapon in current use.
    {

      AddWeaponToInventory(currentWeapon);
      Destroy(currentWeaponModel);

      currentWeapon = null;
    }
    switch (slot)
    {
      case 1:
        currentWeapon = primarySlot1;
        primarySlot1 = null;
        ShowWeapon();

        break;
      case 2:
        currentWeapon = primarySlot2;
        primarySlot2 = null;
        ShowWeapon();
        break;


    }

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

      }
      currentWeapon = secondarySlot1;
      secondarySlot1 = null;
      print("Using " + currentWeapon.name);

    }
    ShowWeapon();
  }

  void ShowWeapon()
  {
    if (currentWeapon != null)
    {
      if (currentWeaponModel == null)
      {
        currentWeaponModel = Instantiate(currentWeapon.WeaponModel, weaponPlacementParent.transform.position, Quaternion.identity, weaponPlacementParent.transform);
        uIManager.ClipCountText(currentWeapon.CurrentAmmo);

      }
      else
      {
        Destroy(currentWeaponModel);
        currentWeaponModel = Instantiate(currentWeapon.WeaponModel, weaponPlacementParent.transform.position, Quaternion.identity, weaponPlacementParent.transform);
        uIManager.ClipCountText(currentWeapon.CurrentAmmo);

      }
    }
    else
    {
      return;
    }
  }

}
