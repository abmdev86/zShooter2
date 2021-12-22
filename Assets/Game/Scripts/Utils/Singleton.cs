using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
  public static T Instance
  {
    get
    {
      if (_instance = null)
      {
        _instance = FindOrCreateInstance();
      }
      return _instance;
    }
  }
  static T _instance;

  private static T FindOrCreateInstance()
  {
    T instance = GameObject.FindObjectOfType<T>();
    if (instance != null)
    {
      return instance;
    }

    // create new instance
    string name = typeof(T).Name + " Singleton";
    GameObject go = new GameObject(name);
    T singletonComponent = go.AddComponent<T>();
    return singletonComponent;
  }


}

