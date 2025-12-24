using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    Rigidbody rigid;

    float speed = 1f;

    // NevMesh
    Transform player;
    Transform patrolRoute;
    List<Transform> locations;
    NavMeshAgent agent;
    int locationIndex = 0;

    [Header("HP")]
    public int maxHp = 10;
    int hp;
    int damage;

    [Header("Bullet")]
    public GameObject bullet;
    public float bulletSpeed = 100f;
    public float fireRate = 1f;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();

        player = GameObject.Find("Player").transform;
        patrolRoute = GameObject.Find("PatrolRoute").transform;

        locations = new List<Transform>();
    }

    void Start()
    {
        hp = maxHp;
        // InitializePatroalRoute();
        // MoveToNextPatrolLocation();
        StartCoroutine(Shoot());
    }

    void Update()
    {
        if (hp <= 0)
        {
            Debug.Log($"Enemy die");
            Destroy(this.gameObject);
        }

        Move();

        // if (agent.remainingDistance < 0.2f && !agent.pathPending) { MoveToNextPatrolLocation(); }
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
    void InitializePatroalRoute()
    {
        foreach (Transform child in patrolRoute)
        {
            locations.Add(child);
        }
    }

    void MoveToNextPatrolLocation()
    {
        if (locations.Count == 0) { return; }

        agent.destination = locations[locationIndex].position;

        locationIndex = (locationIndex + 1) % locations.Count;
    }
    #endregion

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