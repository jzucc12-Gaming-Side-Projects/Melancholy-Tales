using UnityEngine;
using UnityEngine.UI;

public class ContentSizeFitterUpdater : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Canvas.ForceUpdateCanvases();
        foreach(var layout in GetComponentsInChildren<VerticalLayoutGroup>())
        {
            layout.enabled = false;
            layout.enabled = true;
        }

        foreach(var layout in GetComponentsInChildren<HorizontalLayoutGroup>())
        {
            layout.enabled = false;
            layout.enabled = true;
        }
    }
}
