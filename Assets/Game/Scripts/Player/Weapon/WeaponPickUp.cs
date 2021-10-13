using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickUp : MonoBehaviour
{
  [SerializeField] SOWeapon PickUp;

  private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.tag == "Player")
    {
      WeaponManager playerWM = other.GetComponent<WeaponManager>();
      playerWM.AddWeaponToInventory(PickUp);
      Destroy(gameObject);
    }
  }
}
