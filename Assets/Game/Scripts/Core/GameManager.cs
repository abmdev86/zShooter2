
using UnityEngine;
public class GameManager : Singleton<GameManager>
{

  Spawner spawner;
  [SerializeField] GameObject playerSpawnLocation;
  [SerializeField] GameObject enemySpawnLocation;

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
      UIManager.Instance.AmmoCount = FindObjectOfType<WeaponManager>().CurrentWeapon.CurrentAmmo.ToString();
    }
    print("Player alive: " + IsPlayerAlive);
    print("Game running: " + isGameRunning);


  }


}
