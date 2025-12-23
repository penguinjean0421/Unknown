using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody rigid;

    float speed = 1f;

    public int maxHp = 10;
    int hp;
    int damage;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    void Start()
    {
        hp = maxHp;
    }

    void Update()
    {
        Move();

        if (hp <= 0)
        {
            Debug.Log($"Enemy die");
            Destroy(this.gameObject);
        }
    }

    void Move()
    {
        float vInput = 0;
        vInput += speed;

        Vector3 movement = new Vector3(vInput, 0f, 0f).normalized;
        transform.position += movement * Time.deltaTime;

        if (transform.position.x >= 5) { speed *= -1; }
        if (transform.position.x <= -5) { speed *= -1; }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Bullet(Clone)")
        {
            damage = Random.Range(1, 5);
            hp -= damage;

            Destroy(collision.gameObject);

            Debug.Log($"Enemy Attack. current Hp : {hp}");
            Debug.Log($"score +={damage}");
        }
    }
}