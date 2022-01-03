
using UnityEngine;

namespace com.sluggaGames.gambit.Weapon
{
  public enum WeaponType
  {
    Primary,
    Secondary
  }
  [CreateAssetMenu(fileName = "New Weapon", menuName = "Data/Weapon")]

  public class SOWeapon : ScriptableObject
  {
    public string WeaponName = "Empty";
    public float Range = 0;
    public float Damage = 0;
    public int MaxAmmo = 15;

    public float fireRate = 1.5f;
    public float ReloadSpeed = 1.5f;

    public WeaponType WeaponType;
    public GameObject WeaponModel;
    public ParticleSystem UseFx;

  }
}