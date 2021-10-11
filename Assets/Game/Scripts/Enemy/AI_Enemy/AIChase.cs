using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIChase : MonoBehaviour
{
  public enum AISTATE { PATROL = 0, CHASE = 1, ATTACK = 2 }
  NavMeshAgent agent = null;
  Transform player = null;
  EnemyAvoider avoider;
  public bool TargetIsVisible;
  float attackDistance = 2f;
  [SerializeField]
  AISTATE _currentState = AISTATE.PATROL;
  public AISTATE CurrentState
  {
    get
    {
      return _currentState;
    }
    set
    {
      StopAllCoroutines();
      _currentState = value;

      switch (CurrentState)
      {
        case AISTATE.PATROL:
          StartCoroutine(StatePatrol());
          break;
        case AISTATE.CHASE:
          //StartCoroutine(avoider.ChaseAndAvoid());
          StartCoroutine(StateChase());
          break;
        case AISTATE.ATTACK:
          StartCoroutine(StateAttack());
          break;
      }
    }
  }


  private void Awake()
  {
    agent = GetComponent<NavMeshAgent>();
    player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    avoider = new EnemyAvoider(10, 1);

  }
  private void Start()
  {
    CurrentState = AISTATE.PATROL;
  }

  private void Update()
  {
    TargetIsVisible = transform.CheckVisibility(player);
    print(TargetIsVisible);

  }

  private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.tag == "Player")
    {
      CurrentState = AISTATE.CHASE;
    }
  }
  private void OnTriggerExit(Collider other)
  {
    if (other.gameObject.tag == "Player")
    {
      CurrentState = AISTATE.PATROL;
    }
  }
  public IEnumerator StateChase()
  {

    while (CurrentState == AISTATE.CHASE)
    {
      if (Vector3.Distance(transform.position, player.position) < attackDistance)
      {
        CurrentState = AISTATE.ATTACK;
        break;
      }
      agent.SetDestination(player.position);

      yield return null;
    }

  }
  public IEnumerator StateAttack()
  {
    Vector3 hidingSpot;
    while (CurrentState == AISTATE.ATTACK)
    {
      if (Vector3.Distance(transform.position, player.position) > attackDistance)
      {
        CurrentState = AISTATE.CHASE;
        break;
      }
      print("Attacking");
      EnemyShot();

      if (!avoider.FindHidingSpot(transform, out hidingSpot, agent, transform))
      {
        agent.SetDestination(player.transform.position);
        yield return new WaitForSeconds(1.0f);

      }
      yield return null;
    }
  }

  void EnemyShot()
  {
    RaycastHit hit;
    if (Physics.Raycast(transform.position, transform.forward, out hit, 5f))
    {
      if (hit.collider.tag == "Player")
      {
        ITargetable target = hit.collider.GetComponent<ITargetable>();
        if (target != null)
          target.TakeDamage(5, hit.point, hit.normal);
      }
    }
  }
  public IEnumerator StatePatrol()
  {
    GameObject[] waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
    GameObject currentWaypoint = waypoints[Random.Range(0, waypoints.Length)];
    float targetDistance = 2f;
    while (CurrentState == AISTATE.PATROL)
    {
      agent.SetDestination(currentWaypoint.transform.position);

      if (Vector3.Distance(transform.position, currentWaypoint.transform.position) < targetDistance)
      {
        currentWaypoint = waypoints[Random.Range(0, waypoints.Length)];
      }
      yield return null;
    }

  }




}
