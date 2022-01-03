using UnityEngine;

namespace com.sluggaGames.gambit.Core
{
  [CreateAssetMenu(fileName = "New Character 1", menuName ="Data/Character")]
  public class SOCharacter : ScriptableObject
  {
    public string CharacterName;
    public GameObject Model;

    public float Armor = 30f;
    public float Health = 130f;
    public float MeleeDamage = 50f;

  }
}
