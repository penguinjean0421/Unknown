using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    // UI
    Slider hpSlider;
    Text nameText;

    // NevMesh
    Transform player;
    NavMeshAgent agent;
    // Hp
    public int maxHp;
    int hp;

    // Bullet
    public GameObject bullet;
    public float bulletSpeed = 100f;
    public float fireRate = 1f;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        hpSlider = GetComponentInChildren<Slider>();
        nameText = GetComponentInChildren<Text>();

        player = GameObject.Find("Player").transform;
    }

    void Start()
    {
        hp = maxHp;
        hpSlider.value = (float)hp / maxHp;
        nameText.text = "Enemy Name";

        Debug.Log($"Enemy Hp : {hp}");

        MoveToNextPatrolLocation();
        StartCoroutine(Shoot());
    }

    void Update()
    {
        if (hp <= 0)
        {
            Debug.Log($"Enemy die");
            Die();
        }

        if (agent.remainingDistance < 0.2f && !agent.pathPending) { MoveToNextPatrolLocation(); }
    }

    void Die()
    {
        GameManager.Instance.UpdateExp(GameManager.Instance.killExp);
        Destroy(this.gameObject);
    }

    #region Shoot
    void ShootBullet()
    {
        GameObject newBullet = Instantiate(bullet, this.transform.position + new Vector3(0, 0, -1), this.transform.rotation);
        Rigidbody bulletRb = newBullet.GetComponent<Rigidbody>();
        bulletRb.linearVelocity = -this.transform.forward * bulletSpeed;
    }

    IEnumerator Shoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(fireRate);
            ShootBullet();
        }
    }
    #endregion

    #region Navmesh
    void MoveToNextPatrolLocation()
    {
        float xPos = Random.Range(-50, 50);
        float zPos = Random.Range(-50, 50);
        agent.destination = new Vector3(xPos, transform.position.y, zPos);
    }
    #endregion

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            int damage = Random.Range(1, 5);
            hp -= damage;

            hpSlider.value = (float)hp / (float)maxHp;

            Destroy(collision.gameObject);

            Debug.Log($"Enemy Attack. current Hp : {hp}");
            GameManager.Instance.GetScore(damage);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            agent.destination = player.position;
            Debug.Log("Player detected - attack!");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            Debug.Log("Player out of range, resume patrol");
        }
    }
}