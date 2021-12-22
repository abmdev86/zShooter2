
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
  Text ammoCountText;
  public string AmmoCount;

  private void Awake()
  {
    ammoCountText = FindObjectOfType<Text>();
    if (ammoCountText == null)
    {
      print("missing ammoCountText");
    }

  }
  private void Update()
  {
    ammoCountText.text = AmmoCount;

  }

  public void ClipCountText(int value)
  {
    ammoCountText.text = value.ToString();

  }

}
