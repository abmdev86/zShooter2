using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickUp : MonoBehaviour
{
  [SerializeField] SOWeapon pickUp;
  ParticleSystem pickupFX;
  private void Awake()
  {
    pickupFX = GetComponent<ParticleSystem>();
  }
  private void Start()
  {
    Instantiate(pickUp.WeaponModel, transform.position, Quaternion.identity, transform);
    pickupFX.Play();

  }

  private void Update()
  {
    transform.Rotate(0.2f, 0.2f, 0.2f, Space.Self);
  }
  private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.tag == "Player")
    {
      WeaponManager playerWM = other.GetComponent<WeaponManager>();
      playerWM.AddWeaponToInventory(pickUp);
      Destroy(gameObject);
    }
  }
}
