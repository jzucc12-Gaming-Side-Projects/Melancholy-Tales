using UnityEngine;

public class Destroyer : MonoBehaviour
{
    public void DestroyParent()
    {
        Destroy(transform.parent.gameObject);
    }
}
