using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    [Header("Move")]
    Rigidbody rigid;
    public float speed = 2f;
    public float maxSpeed = 5f;

    [Header("Weapon")]
    Vector3 leftMuzzle, rightMuzzle;
    public GameObject bullet;
    public float bulletSpeed = 100f;
    bool isShooting;

    public
    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    void Start()
    {
        leftMuzzle = GameObject.Find("LeftMuzzle").transform.position;
        rightMuzzle = GameObject.Find("RighttMuzzle").transform.position;
    }

    void Update()
    {
        if (Keyboard.current == null) { return; }

        if (Keyboard.current.spaceKey.isPressed) { isShooting = true; }

        if (Keyboard.current.wKey.isPressed) { rigid.AddForce(0, 0, speed, ForceMode.Force); }
        if (rigid.linearVelocity.z >= maxSpeed) { rigid.linearVelocity = new Vector3(rigid.linearVelocity.x, rigid.linearVelocity.y, maxSpeed); }

        if (Keyboard.current.sKey.isPressed) { rigid.AddForce(0, 0, -speed, ForceMode.Force); }
        if (rigid.linearVelocity.z <= -maxSpeed) { rigid.linearVelocity = new Vector3(rigid.linearVelocity.x, rigid.linearVelocity.y, -maxSpeed); }

        if (Keyboard.current.aKey.isPressed) { rigid.AddForce(-speed, 0, 0, ForceMode.Force); }
        if (rigid.linearVelocity.x <= -maxSpeed) { rigid.linearVelocity = new Vector3(-maxSpeed, rigid.linearVelocity.y, rigid.linearVelocity.z); }

        if (Keyboard.current.dKey.isPressed) { rigid.AddForce(speed, 0, 0, ForceMode.Force); }
        if (rigid.linearVelocity.x >= maxSpeed) { rigid.linearVelocity = new Vector3(maxSpeed, rigid.linearVelocity.y, rigid.linearVelocity.z); }

        Debug.Log($"Velocity : {rigid.linearVelocity}");

        if (isShooting)
        {
            GameObject rightBullet = Instantiate(bullet, rightMuzzle, this.transform.rotation);
            Rigidbody rightBulletRb = rightBullet.GetComponent<Rigidbody>();
            rightBulletRb.linearVelocity = this.transform.forward * bulletSpeed;

            GameObject leftBullet = Instantiate(bullet, leftMuzzle, this.transform.rotation);
            Rigidbody leftBulletRb = leftBullet.GetComponent<Rigidbody>();
            leftBulletRb.linearVelocity = this.transform.forward * bulletSpeed;
        }
        isShooting = false;
    }
}