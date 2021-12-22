
using UnityEngine;
using UnityEngine.UI;

public  class UIManager : MonoBehaviour
{
  [SerializeField] Text ammoCountText;
  public string AmmoCount = "Default";

  private void Awake()
  {
    ammoCountText = FindObjectOfType<Text>();
    if (ammoCountText == null)
    {
      print("missing ammoCountText");
    }

  }

  public void AmmoCountText(int value)
  {
    ammoCountText.text = value.ToString();

  }

}
