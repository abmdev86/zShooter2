
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

  Spawner spawner;
  [SerializeField] GameObject playerSpawnLocation;
  [SerializeField] GameObject enemySpawnLocation;
  [SerializeField] Text ammoCountText;




  GameObject player;
  [SerializeField]
  bool isGameRunning = true;
  [SerializeField]
  bool isPlayerAlive;
  public bool IsPlayerAlive
  {
    get
    {
      return isPlayerAlive;
    }
    set
    {

      isPlayerAlive = value;
      if (value == false)
      {
        isGameRunning = false;
      }
    }
  }


  private void Awake()
  {

    spawner = Resources.Load("spawner", typeof(Spawner)) as Spawner;
    player = Resources.Load("player", typeof(GameObject)) as GameObject;
    IsPlayerAlive = true;

  }

  private void Start()
  {
    if (spawner != null)
    {

      spawner.SpawnObject(player, playerSpawnLocation.transform.position);
    }
  }

  private void Update()
  {
//    print(GameObject.FindGameObjectWithTag("Weapon").GetComponent<Weapon>().AmmoCount);
    if (isGameRunning)
    {

      spawner.Spawn(1, 5f, enemySpawnLocation, gameObject);
    //  ammoCountText.text = GameObject.FindGameObjectWithTag("Weapon").GetComponent<Weapon>().AmmoCount.ToString();
    }
    print("Player alive: " + IsPlayerAlive);
    print("Game running: " + isGameRunning);


  }


}
