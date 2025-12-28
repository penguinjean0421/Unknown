using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // 
    GameObject title;
    GameObject inGame;
    internal GameObject gameOver;

    // Score
    internal Text scoreText;

    // Level
    internal Text levelText;

    // Exp
    internal Slider expSlider;
    internal Text expText;

    // hp
    internal Slider hpSlider;
    internal Text hpText;

    void Awake()
    {
        title = GameObject.Find("Title");
        inGame = GameObject.Find("InGame");
        gameOver = GameObject.Find("GameOver");

        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        levelText = GameObject.Find("LevelText").GetComponent<Text>();

        expSlider = GameObject.Find("ExpSlider").GetComponent<Slider>();
        expText = GameObject.Find("ExpText").GetComponent<Text>();

        hpSlider = GameObject.Find("HpSlider").GetComponent<Slider>();
        hpText = GameObject.Find("HpText").GetComponent<Text>();
    }

    void Start()
    {
        inGame.SetActive(false);
        gameOver.SetActive(false);
    }

    internal void GameOver()
    {
        gameOver.SetActive(true);
    }

    #region  Update UI
    internal void UpdateHpDisPlay(int nowHp, int maxHp)
    {
        hpSlider.value = (float)nowHp / maxHp;
        hpText.text = $"{nowHp}";
    }

    internal void UpdateExpDisPlay(int nowExp, int maxExp)
    {
        if (GameManager.Instance.level >= GameManager.Instance.nextExp.Length)
        {
            expSlider.value = 1f;
            expText.text = "MAX";
        }
        else
        {
            expSlider.value = (float)nowExp / maxExp;
            expText.text = $"{expSlider.value * 100}%";
        }
    }

    internal void UpdateScoreText(int score)
    {
        scoreText.text = $"Score : {score}";
    }

    internal void UpdateLevelText(int level)
    {
        if (GameManager.Instance.level >= GameManager.Instance.nextExp.Length) { levelText.text = $"Level : Max"; }
        else { levelText.text = $"Level : {level + 1}"; }
    }
    #endregion

    #region Buttons
    public void OnClickGameStart()
    {
        title.SetActive(false);
        inGame.SetActive(true);
        GameManager.Instance.GameStart();
    }

    public void OnClickReStart()
    {
        gameOver.SetActive(false);
        inGame.SetActive(true);
        GameManager.Instance.GameStart();
    }

    public void OnClckGameQuit()
    {
        GameManager.Instance.GameQuit();
    }
    #endregion
}