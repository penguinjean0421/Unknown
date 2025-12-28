using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    Weapon weapon;

    // Move Speed
    public float speed = 2f;

    // Hp
    public int maxHp = 100;
    internal int hp;

    void Awake()
    {
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

        weapon.isShooting = Keyboard.current.spaceKey.isPressed;
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
        if (collision.gameObject.tag == "EnemyBullet")
        {
            int damage = Random.Range(1, 3);
            hp -= damage;

            GameManager.Instance.UpdateHp(hp, maxHp);
            Debug.Log($"Player Attack. now hp : {hp}");
        }

        if (collision.gameObject.tag == "DeadZone")
        {
            GameManager.Instance.GameOver();
        }
    }
}