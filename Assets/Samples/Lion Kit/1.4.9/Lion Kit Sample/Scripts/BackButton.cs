using UnityEngine.UI;

public class BackButton : Button
{
    protected override void Start()
    {
        base.Start();
        LionMenu menu = GetComponentInParent<LionMenu>();
        onClick.AddListener(() => menu.gameObject.SetActive(false));
    }
}
