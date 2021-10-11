
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
  [SerializeField] float spawnTimer;
  [SerializeField] List<GameObject> objectSpawnList;
  [SerializeField] int limit;

  private void Start()
  {
    objectSpawnList = new List<GameObject>();

  }

  public void SpawnObject(GameObject objectToSpawn, Vector3 location)
  {
    Instantiate(objectToSpawn, location, Quaternion.identity);

  }
  GameObject GetObject()
  {
    GameObject objectToSpawn;
    int randomPick = Random.Range(0, objectSpawnList.Count);
    objectToSpawn = objectSpawnList[randomPick];
    return objectToSpawn;
  }

  public void Spawn(int numberToSpawn, float rate, GameObject spawnLocation, GameObject parent)
  {


    if (parent.transform.childCount < limit)
    {
      spawnTimer -= Time.deltaTime;

      if (spawnTimer <= 0f)
      {
        for (int i = 0; i < numberToSpawn; i++)
        {
          GameObject objectToSpawn = GetObject();

          Instantiate(objectToSpawn, spawnLocation.transform.position, Quaternion.identity, parent.transform);
        }
        spawnTimer = rate;
      }
    }
  }


}
