using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // Scripts
    UIManager uiManager;
    Player player;

    // Score
    public int damagePoint;
    int score;

    // Level
    public int[] nextExp;
    public int killExp;
    internal int level;
    int exp;

    void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(this.gameObject); }

        uiManager = FindAnyObjectByType<UIManager>();
        player = FindAnyObjectByType<Player>();
    }

    void Start()
    {
        Time.timeScale = 0f;
    }

    public void GetScore(int damage)
    {
        score += damage * damagePoint;

        uiManager.UpdateScoreText(score);
    }

    public void UpdateHp(int nowHp, int maxHp)
    {
        uiManager.UpdateHpDisPlay(nowHp, maxHp);
    }

    // Level Up
    public void UpdateExp(int amount)
    {
        exp += amount;

        while (level < nextExp.Length && exp >= nextExp[level])
        {
            exp -= nextExp[level];
            level++;

            if (level >= nextExp.Length)
            {
                exp = 0;
                break;
            }
        }

        if (level < nextExp.Length) { uiManager.UpdateExpDisPlay(exp, nextExp[level]); }
        else { uiManager.UpdateExpDisPlay(exp, exp > 0 ? exp : 1); }
    }

    // Game End
    internal void GameOver()
    {
        Time.timeScale = 0f;
        uiManager.GameOver();
    }

    #region Buttons Func
    internal void GameStart()
    {
        score = 0;
        exp = 0;
        level = 0;

        uiManager.UpdateHpDisPlay(player.hp, player.maxHp);
        uiManager.UpdateExpDisPlay(exp, nextExp[level]);
        uiManager.UpdateScoreText(score);

        Time.timeScale = 1f;
    }

    internal void ReStart()
    {
        score = 0;
        exp = 0;
        level = 0;

        uiManager.UpdateHpDisPlay(player.hp, player.maxHp);
        uiManager.UpdateExpDisPlay(exp, nextExp[level]);
        uiManager.UpdateScoreText(score);

        Time.timeScale = 1f;
    }

    internal void GameQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        Debug.Log("Game Quit");
#endif

        Application.Quit();
    }
    #endregion
}