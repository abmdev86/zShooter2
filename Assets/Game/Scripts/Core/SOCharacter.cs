using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.sluggaGames.gambit.Core
{
  [CreateAssetMenu(fileName = "New Character 1", menuName ="Data/Character")]
  public class SOCharacter : ScriptableObject
  {
    public string CharacterName;
    public GameObject Model;
    public SOWeapon WeaponSlot1 = null;
    public SOWeapon WeaponSlot2 = null;
    public SOWeapon WeaponSlot3 = null;

    public float Armor = 30f;


  }
}
