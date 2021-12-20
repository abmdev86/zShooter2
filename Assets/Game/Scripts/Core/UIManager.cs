using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
  [SerializeField] Text ammoCountText;


  public void ClipCountText(int value)
  {
    ammoCountText.text = value.ToString();

  }

}
