using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, ITargetable
{
  [SerializeField] float maxHealth = 100f;
  [SerializeField]
  float currentHealth;
  public float CurrentHealth
  {
    get
    {
      return currentHealth;
    }
    set
    {
      if (currentHealth <= 0)
      {
        Die();
      }
      currentHealth = value;
    }
  }
  [SerializeField]
  GameObject hitEffect;

  public void Die()
  {
    Destroy(gameObject);
  }

  public void TakeDamage(float dmg, Vector3 hitPos, Vector3 hitNormal)
  {
    Instantiate(hitEffect, hitPos, Quaternion.LookRotation(hitNormal));
    CurrentHealth -= dmg;
  }
}
