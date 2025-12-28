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

    // level
    internal Slider levelSlider;
    internal Text levelText;

    // hp
    internal Slider hpSlider;
    internal Text hpText;

    void Awake()
    {
        title = GameObject.Find("Title");
        inGame = GameObject.Find("InGame");
        gameOver = GameObject.Find("GameOver");

        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        levelSlider = GameObject.Find("LevelSlider").GetComponent<Slider>();
        levelText = GameObject.Find("LevelText").GetComponent<Text>();
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
            levelSlider.value = 1f;
            levelText.text = "MAX";
        }
        else
        {
            levelSlider.value = (float)nowExp / maxExp;
            levelText.text = $"{levelSlider.value * 100}%";
        }
    }

    internal void UpdateScoreText(int score)
    {
        scoreText.text = $"Score : {score}";
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

    }

    public void OnClckGameQuit()
    {
        GameManager.Instance.GameQuit();
    }
    #endregion
}