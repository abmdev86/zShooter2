using System;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{

  private static readonly Lazy<T> LazyInstance = new Lazy<T>(FindOrCreateInstance);
  public static T Instance => LazyInstance.Value;

  private static T FindOrCreateInstance()
  {
    T instance = FindObjectOfType<T>();
   
    // create new instance
    string name = typeof(T).Name + " Singleton";
    GameObject go = new GameObject(name);
    T singletonComponent = go.AddComponent<T>();
    print("added " + singletonComponent.name + " to " + go.name);
    return singletonComponent;
  }


}

