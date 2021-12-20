#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

/// <summary>
/// Detects when a given target is visible to this object. Visible if in range and in front of the target.
/// </summary>
public class EnemyVisibility : MonoBehaviour
{
  Transform target = null;
  public Transform Target{
    get{
      return target;

    }

  }
  [SerializeField] float maxDistance = 10f;
  public float MaxDistance
  {
    get { return maxDistance; }
    set
    {
      maxDistance = value;
    }
  }
  [Range(0f, 360f)]
  public float angle = 45f;
  [SerializeField] bool visualize = true; //show vision in editor
  public bool TargetIsVisible { get; private set; }
  private void Start()
  {
    target = GameObject.FindGameObjectWithTag("Player").transform;
  }

  private void Update()
  {
    TargetIsVisible = CheckVisibility();
    if (visualize)
    {
      Color color = TargetIsVisible ? Color.green : Color.red;
      GetComponent<Renderer>().material.color = color;
    }
    // Debug.Log("Target Located: " + TargetIsVisible);
  }

  public bool CheckVisibilityToPoint(Vector3 worldPoint)
  {
    Vector3 directionToTarget = worldPoint - transform.position;
    float degressToTarget = Vector3.Angle(transform.forward, directionToTarget);
    bool withinArc = degressToTarget < (angle / 2);
    if (withinArc == false)
    {
      return false;
    }
    float distanceToTarget = directionToTarget.magnitude;
    float rayDistance = Mathf.Min(maxDistance, distanceToTarget);
    Ray ray = new Ray(transform.position, directionToTarget);
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

  public bool CheckVisibility()
  {
    
    Vector3 directionToTarget = target.position - transform.position;
    float degreesToTarget = Vector3.Angle(transform.forward, directionToTarget);
    bool withinArc = degreesToTarget < (angle / 2);
    if (withinArc == false)
    {
      return false;
    }
    float distanceToTarget = directionToTarget.magnitude;
    float rayDistance = Mathf.Min(maxDistance, distanceToTarget);
    Ray ray = new Ray(transform.position, directionToTarget);
    RaycastHit hit;
    bool canSee = false;
    if (Physics.Raycast(ray, out hit, rayDistance))
    {
      if (hit.collider.transform == target)
      {
//        print("Hey its seeing the target");
        canSee = true;
      }
      Debug.DrawLine(transform.position, hit.point);
    }
    else
    {
      Debug.DrawRay(transform.position, directionToTarget.normalized * rayDistance);
    }
    return canSee;

  }
}
#if UNITY_EDITOR
[CustomEditor(typeof(EnemyVisibility))]
public class EnemyVisibilityEditor : Editor
{
  private void OnSceneGUI()
  {
    EnemyVisibility visibility = target as EnemyVisibility;
    Handles.color = new Color(1, 1, 1, 0.1f);
    Vector3 forwardPointMinusHalfAngle = Quaternion.Euler(0, -visibility.angle / 2, 0) * visibility.transform.forward;
    Vector3 arcStart = forwardPointMinusHalfAngle * visibility.MaxDistance;

    Handles.DrawSolidArc(visibility.transform.position, Vector3.up, arcStart, visibility.angle, visibility.MaxDistance);

    Handles.color = Color.white;
    Vector3 handlePosition = visibility.transform.position + visibility.transform.forward * visibility.MaxDistance;
    visibility.MaxDistance = Handles.ScaleValueHandle(visibility.MaxDistance, handlePosition, visibility.transform.rotation, 1, Handles.ConeHandleCap, 0.25f);


  }
}
#endif