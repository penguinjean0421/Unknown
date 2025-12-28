using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    Rigidbody rigid;
    Weapon weapon;

    public float speed = 2f;

    public int maxHp = 100;
    internal int hp;
    int damage;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        weapon = GameObject.Find("Weapon").GetComponent<Weapon>();
    }

    void Start()
    {
        hp = maxHp;
    }

    void Update()
    {
        if (hp <= 0) { GameManager.Instance.GameOver(); }

        Move();

        if (Keyboard.current.spaceKey.isPressed) { weapon.isShooting = true; }
    }

    void Move()
    {
        float vInput = 0;
        float hInput = 0;

        if (Keyboard.current.wKey.isPressed) hInput += 1f;
        if (Keyboard.current.sKey.isPressed) hInput -= 1f;

        if (Keyboard.current.aKey.isPressed) vInput -= 1f;
        if (Keyboard.current.dKey.isPressed) vInput += 1f;

        Vector3 movement = new Vector3(vInput, 0f, hInput).normalized;
        transform.position += movement * speed * Time.deltaTime;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Bullet(Clone)")
        {
            damage = Random.Range(1, 3);
            hp -= damage;

            GameManager.Instance.UpdateHp(hp, maxHp);
            Debug.Log($"Player Attack. now hp : {hp}");
        }
    }
}