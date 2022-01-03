
using UnityEngine;

namespace com.sluggaGames.gambit.Core
{
  public class PlayerSpawner : MonoBehaviour
  {
    SOCharacter characterData;
    GameObject player;

    void Start()
    {
      CreatePlayerFromData();
    }
    void CreatePlayerFromData()
    {
      characterData = Object.Instantiate(Resources.Load("Data/Player/Player_Default")) as SOCharacter;
      player = GameObject.Instantiate(characterData.Model) as GameObject;
    }
  }
}