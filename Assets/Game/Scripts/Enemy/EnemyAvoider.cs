
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyAvoider
{
  // [SerializeField] EnemyVisibility visibility = null;
  float searchAreaSize;
  float searchCellSize;



  public EnemyAvoider(float areaSize, float cellSize)
  {
    this.searchAreaSize = areaSize;
    this.searchCellSize = cellSize;
  }

  /// <summary>
  /// Attempts to find a nearby place where the target cant see us. returns true if on was found. if it was put the position in
  /// the hiding spot variable
  /// </summary>
  /// <param name="hidingSpot">Spot to hide</param>
  /// <returns></returns>
  public bool FindHidingSpot(Transform trans, out Vector3 hidingSpot, NavMeshAgent agent, Transform target)
  {
    PoissonDiscSampler disturbution = new PoissonDiscSampler(searchAreaSize, searchAreaSize, searchCellSize);
    List<Vector3> candidateHidingSpots = new List<Vector3>();
    foreach (Vector2 point in disturbution.Samples())
    {
      //Debug.Log("Finding Hiding spot");
      Vector2 searchPoint = point;
      // reposition point so that the middle of the search area is at (0,0)
      searchPoint.x -= searchAreaSize / 2f;
      searchPoint.y -= searchAreaSize / 2f;
      Vector3 searchPointLocalSpace = new Vector3(searchPoint.x, trans.localPosition.y, searchPoint.y);

      Vector3 searchPointWorldSpace = trans.TransformPoint(searchPointLocalSpace);
      //find nearest point on navmesh
      NavMeshHit hit;
      bool foundPoint;
      foundPoint = NavMesh.SamplePosition(searchPointWorldSpace, out hit, 5, NavMesh.AllAreas);
      if (foundPoint == false)
      {
        // cant get to location disregard point to hide.
        continue;
      }

      searchPointWorldSpace = hit.position;
      //bool canSee = visibility.CheckVisibilityToPoint(searchPointWorldSpace);
      bool canSee = trans.CheckVisibilityToPoint(searchPointWorldSpace, target);
      if (canSee == false)
      {
        //cant see target from this point return it
        candidateHidingSpots.Add(searchPointWorldSpace);

      }
      // if (visualize)
      // {
      //   Color debugColor = canSee ? Color.red : Color.green;
      //   Debug.DrawLine(transform.position, searchPointWorldSpace, debugColor, 0.1f);
      // }

    }
    if (candidateHidingSpots.Count == 0)
    {
      // no hiding spots provide a dummy value
      hidingSpot = Vector3.zero;
      return false;
    }
    // for each point calculate the length of path to reach it
    // build a list of candidate points matched with the path distance
    List<KeyValuePair<Vector3, float>> paths;
    //for each point calculate the length
    paths = candidateHidingSpots.ConvertAll((Vector3 point) =>
    {
      NavMeshPath path = new NavMeshPath();
      agent.CalculatePath(point, path);
      float distance;
      if (path.status != NavMeshPathStatus.PathComplete)
      {
        // path does not reach target consider it inifinity far away
        distance = Mathf.Infinity;
      }
      else
      {
        // get up to 32 of the points on this path
        Vector3[] corners = new Vector3[32];
        int cornerCount = path.GetCornersNonAlloc(corners);

        // start with the first point
        Vector3 current = corners[0];
        distance = 0;

        // figure out cumulative distance for each point
        for (int c = 1; c < cornerCount; c++)
        {
          Vector3 next = corners[c];
          distance += Vector3.Distance(current, next);
          current = next;
        }
      }
      return new KeyValuePair<Vector3, float>(point, distance);
    });

    // sort list based on distance, so that the shortest path is at the front
    paths.Sort((a, b) =>
    {
      return a.Value.CompareTo(b.Value);
    });

    // return the point that the shortest to reach
    hidingSpot = paths[0].Key;
    // Debug.Log(hidingSpot);
    return true;
  }


}
