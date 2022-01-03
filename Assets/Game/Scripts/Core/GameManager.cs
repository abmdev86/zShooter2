
using UnityEngine;
namespace com.sluggaGames.gambit.Core
{
  public class GameManager : MonoBehaviour
  {
    static GameManager _instance;
    public static GameManager Instance
    {
      get { return _instance; }
    }
    bool playerDied = false;
    public bool PlayerDied
    {
      get { return playerDied; }
      set
      {
        playerDied = value;
      }
    }
    public static int CurrentScene = 0;
    public static int GameLevel = 3;
    public static int PlayerLives = 3;

    private void Awake()
    {
      CheckForGameManager();
    }

    private void CheckForGameManager()
    {
      if (_instance == null)
      {
        _instance = this;
      }
      else
      {
        Destroy(this.gameObject);
      }
      DontDestroyOnLoad(this);
    }

  }
}