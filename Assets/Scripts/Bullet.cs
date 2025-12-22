using UnityEngine;

public class Bullet : MonoBehaviour
{
    void Start()
    {
        Destroy(this.gameObject, 0.5f);
    }
}
