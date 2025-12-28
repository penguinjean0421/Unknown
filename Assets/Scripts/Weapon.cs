using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bullet;
    GameObject leftMuzzle;
    GameObject rightMuzzle;

    public float bulletSpeed = 100f;
    internal bool isShooting;

    void Awake()
    {
        leftMuzzle = GameObject.Find("LeftMuzzle");
        rightMuzzle = GameObject.Find("RightMuzzle");
    }

    void FixedUpdate()
    {
        if (isShooting) { Shoot(); }
    }

    internal void Shoot()
    {
        GameObject leftBullet = Instantiate(bullet, leftMuzzle.transform.position, this.transform.rotation);
        Rigidbody leftBulletRb = leftBullet.GetComponent<Rigidbody>();
        leftBulletRb.linearVelocity = this.transform.forward * bulletSpeed;

        GameObject rightBullet = Instantiate(bullet, rightMuzzle.transform.position, this.transform.rotation);
        Rigidbody rightBulletRb = rightBullet.GetComponent<Rigidbody>();
        rightBulletRb.linearVelocity = this.transform.forward * bulletSpeed;
    }
}