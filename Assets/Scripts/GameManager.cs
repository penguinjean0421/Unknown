using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // Score
    public int killPoint;
    public int damagePoint;
    int score;

    void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(this.gameObject); }
    }

    void Start()
    {
        score = 0;
    }

    public void GetScore(int enemyHp, int damage)
    {
        if (enemyHp <= 0) { score += killPoint; }

        score += damage * damagePoint;

        Debug.Log($"now Score : {score}");
    }
}