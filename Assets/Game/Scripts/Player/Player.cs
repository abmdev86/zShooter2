using com.sluggaGames.gambit.Core;
using UnityEngine;

namespace com.sluggaGames.gambit.PlayerSystem
{
  public class Player : MonoBehaviour, ICharacterTemplate
  {
    float health;

    public float Health
    {
      get
      {
        return health;
      }
      set
      {
        health = value;
      }
    }
    float armor;
    public float Armor
    {
      get { return armor; }
      set { armor = value; }
    }
    float meleeDamage;
    public float MeleeDamage
    {
      get { return meleeDamage; }
    }

    public void CharacterStats(SOCharacter character)
    {
      health = character.Health;
      armor = character.Armor;
      meleeDamage = character.MeleeDamage;
      gameObject.name = character.name;
    }
  }
}