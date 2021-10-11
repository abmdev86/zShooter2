using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, ITargetable
{
  [SerializeField] float maxHealth = 100f;
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

  private void Awake()
  {
    currentHealth = maxHealth;
  }

  [SerializeField] GameObject hitEffect;


  public void TakeDamage(float dmg, Vector3 hitPos, Vector3 hitNormal)
  {
    Instantiate(hitEffect, hitPos, Quaternion.LookRotation(hitNormal));
    CurrentHealth -= dmg;

  }

  public void Die()
  {
    print("player died");
    
  }
}
