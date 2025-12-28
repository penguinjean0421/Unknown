using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float deleteTime;

    void Start()
    {
        Destroy(this.gameObject, deleteTime);
    }
}
