
using UnityEngine;

public interface ITargetable
{
  void Die();
  void TakeDamage(float dmg, Vector3 hitPos, Vector3 hitNormal);

}
