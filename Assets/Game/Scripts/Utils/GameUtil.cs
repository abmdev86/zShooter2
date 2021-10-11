using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameUtil
{

  public static bool CheckVisibilityToPoint(this Transform trans, Vector3 worldPoint, Transform target, float angle = 45, float maxDistance = 10f)
  {
    Vector3 directionToTarget = worldPoint - trans.position;

    float degressToTarget = Vector3.Angle(trans.forward, directionToTarget);
    bool withinArc = degressToTarget < (angle / 2);
    if (withinArc == false)
    {
      return false;
    }
    float distanceToTarget = directionToTarget.magnitude;
    float rayDistance = Mathf.Min(maxDistance, distanceToTarget);
    Ray ray = new Ray(trans.position, directionToTarget);
    RaycastHit hit;
    if (Physics.Raycast(ray, out hit, rayDistance))
    {
      if (hit.collider.transform == target)
      {
        return true;

      }
      return false;
    }
    else
    {
      return true;
    }
  }


  public static bool CheckVisibility(this Transform trans, Transform target, float angle = 45, float maxDistance = 10f)
  {
    //Debug.Log("Checking Visibility");
    Vector3 directionToTarget = target.position - trans.position;
    float degreesToTarget = Vector3.Angle(trans.forward, directionToTarget);
    bool withinArc = degreesToTarget < (angle / 2);
    if (withinArc == false)
    {
      return false;
    }
    float distanceToTarget = directionToTarget.magnitude;
    float rayDistance = Mathf.Min(maxDistance, distanceToTarget);
    Ray ray = new Ray(trans.position, directionToTarget);
    RaycastHit hit;
    bool canSee = false;
    if (Physics.Raycast(ray, out hit, rayDistance))
    {
      if (hit.collider.transform == target)
      {
        //        print("Hey its seeing the target");
        canSee = true;
      }
      Debug.DrawLine(trans.position, hit.point);
    }
    else
    {
      Debug.DrawRay(trans.position, directionToTarget.normalized * rayDistance);
    }
    return canSee;

  }
}
