using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
#pragma warning disable 649
  Transform cam;
  [SerializeField] float range = 50f;
  [SerializeField] float damage = 10f;
  [SerializeField] Transform muzzlePoint;
  [SerializeField] ParticleSystem muzzleFlash;
  [SerializeField] int ammoCount = 15;
  [SerializeField] int magSize = 15;
  [SerializeField] int magCount = 10;
  [SerializeField] float reloadSpeed = 1.5f;


  public int AmmoCount
  {
    get
    {
      return ammoCount;
    }
    set
    {

      ammoCount = value;
    }
  }

  private void Awake()
  {
    cam = Camera.main.transform;


  }

  public void Shoot()
  {
    if (ammoCount <= 0)
    {
      Reload();
      Debug.LogWarning("Hook up reload to a key press as well");


      return;
    }
    RaycastHit hit;
    muzzleFlash.Play();
    --ammoCount;
    //print(ammoCount);
    if (Physics.Raycast(cam.position, cam.forward, out hit, range))
    {


      if (hit.collider.GetComponent<ITargetable>() != null)
      {
        ITargetable target = hit.collider.GetComponent<ITargetable>();
        target.TakeDamage(damage, hit.point, hit.normal);
      }
    }

  }

  public void Reload()
  {
    if (ammoCount == magSize) return;
    if (magCount == 0)
    {
      print("no magazines left");
      return;
    }
    StartCoroutine(ReloadEnum());

  }

  IEnumerator ReloadEnum()
  {
    print("reloading");
    yield return new WaitForSeconds(reloadSpeed);
    --magCount;
    ammoCount = magSize;
  }
}
