using UnityEngine;
using com.sluggaGames.gambit.PlayerSystem;

namespace com.sluggaGames.gambit.Core
{
  public class PlayerSpawner : MonoBehaviour
  {
    SOCharacter characterData;
    GameObject player;
    [SerializeField]
    GameObject spawnLocation;

    void Start()
    {
      CreatePlayerFromData();
    }
    void SetPlayer()
    {
      player.transform.SetParent(this.transform);
      player.transform.position = spawnLocation.transform.position;
    }
    void CreatePlayerFromData()
    {
      characterData = Object.Instantiate(Resources.Load("Data/Player/Player_Default")) as SOCharacter;
      player = GameObject.Instantiate(characterData.Model) as GameObject;
      player.GetComponent<Player>().CharacterStats(characterData);
    }
  }
}