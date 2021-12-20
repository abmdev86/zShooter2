using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
  Primary,
  Secondary
}
[CreateAssetMenu(fileName = "WeaponData", menuName = "GameWeapons/Create Weapon")]

public class SOWeapon : ScriptableObject
{
  public string WeaponName = "Empty";
  public float Range = 0;
  public float Damage = 0;
  [SerializeField]
  private int maxAmmo = 15;
  public int MaxAmmo
  {
    get
    {
      return maxAmmo;
    }
  }
  private int currentAmmo;
  public int CurrentAmmo
  {
    get
    {
      return currentAmmo;
    }
  }
  public float fireRate = 1.5f;
  public float ReloadSpeed = 1.5f;

  public WeaponType WeaponType;
  public GameObject WeaponModel;
  public ParticleSystem UseFx;

  public void InitWeapon()
  {
    currentAmmo = maxAmmo;
    Debug.Log("Initiating " + WeaponName);
  }

  public void Reload(int ammoPool)
  {
    Debug.Log("Reloading...");
    // if (ammoPool <= 0)
    // {
    //   return;
    // }
    // if (ammoPool < ClipSize)
    // {
    //   CurrentClipCount = ammoPool;
    //   ammoPool = 0;
    // }
    // ammoPool -= ClipSize;
    // CurrentClipCount = ClipSize;
  }






}
