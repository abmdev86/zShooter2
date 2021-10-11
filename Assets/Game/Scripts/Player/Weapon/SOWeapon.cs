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
  public int ClipSize = 15;
  public float CoolDown = 1.5f;
  public float ReloadSpeed = 1.5f;

  public WeaponType WeaponType;
  public GameObject WeaponModel;
  public ParticleSystem UseFx;






}
