using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text clickCountText;
    public TMP_Text highScoreText;
    public TMP_Text timerText;
    public Button clickButton;
    public Button restartButton;

    [Header("Ads")]
    public AdsManager adsManager;

    private int clickCount = 0;
    private int highScore = 0;
    private float timer = 10f;
    private bool gameActive = true;
    private bool estaPausado = false;
    private bool tiempoExtraPorReward = false;

    void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = "HighScore: " + highScore;
        clickCountText.text = "You've pressed the button 0 times";
        timerText.text = "Time left: 10";

        restartButton.gameObject.SetActive(false);
        clickButton.onClick.AddListener(IncrementClick);
        restartButton.onClick.AddListener(ReiniciarConReward);
    }

    void Update()
    {
        if (!gameActive || estaPausado) return;

        timer -= Time.deltaTime;
        timerText.text = "Time left: " + Mathf.CeilToInt(timer).ToString();

        if (timer <= 0)
        {
            EndGame();
        }
    }

    void IncrementClick()
    {
        if (!gameActive || estaPausado) return;

        clickCount++;
        clickCountText.text = "You've pressed the button " + clickCount + " times";
    }

    void EndGame()
    {
        gameActive = false;
        clickButton.interactable = false;
        restartButton.gameObject.SetActive(true);

        bool nuevoRecord = false;

        if (clickCount > highScore)
        {
            highScore = clickCount;
            PlayerPrefs.SetInt("HighScore", highScore);
            highScoreText.text = "HighScore: " + highScore;
            nuevoRecord = true;
        }

        if (!nuevoRecord)
        {
            adsManager.interstitial.ShowAd();
        }
    }

    void ReiniciarConReward()
    {
        if (adsManager.rewarded.RewardFueOtorgado())
        {
            tiempoExtraPorReward = true;
        }

        RestartGame();
    }

    void RestartGame()
    {
        clickCount = 0;
        timer = tiempoExtraPorReward ? 12f : 10f;
        tiempoExtraPorReward = false;
        gameActive = true;
        estaPausado = false;

        clickCountText.text = "You've pressed the button 0 times";
        timerText.text = "Time left: " + timer.ToString();
        clickButton.interactable = true;
        restartButton.gameObject.SetActive(false);
    }

    public void PausarJuego(bool pausar)
    {
        estaPausado = pausar;
        clickButton.interactable = !pausar;
    }
}
