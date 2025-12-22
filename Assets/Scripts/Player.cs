using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    Rigidbody rigid;
    public float speed = 2f;
    Weapon weapon;

    public
    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        weapon = GameObject.Find("Weapon").GetComponent<Weapon>();
    }
    void Update()
    {
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

}