using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField]
    private TextMeshProUGUI CoinText;
    private int coin;
    private int coinDifferenceCheck;
    bool isThereCoin;

    private int stageFloor;
    public TextMeshProUGUI StageText;

    public GameObject pauseHUD;

    public Button PauseUI;

    float testTimer = 0;

    protected override void Awake()
    {
        base.Awake();

        coin = 0;
        CoinText = GameObject.FindWithTag("Coin").GetComponent<TextMeshProUGUI>();
        CoinText.text = $"{coin}";
        isThereCoin = (CoinText != null) ? true : false;

        stageFloor = 0;
        StageText.text = $"{stageFloor}";

        PauseUI = GetComponent<Button>();
    }

    private void Start()
    {
        // Test
        InvokeRepeating(nameof(AcendStage), 1f, 1f);
    }

    private void Update()
    {
        // Test
        testTimer += Time.deltaTime;
        if (testTimer >= 1f)
        {
            PlusCoin(100);
            testTimer = 0;
        }
    }

    #region Coin
    public void UpdateCoin()
    {
        if (coin != coinDifferenceCheck && isThereCoin)
            CoinText.text = $"{coin}";
    }

    public void PlusCoin(int coin)
    {
        this.coin += coin;
        if (isThereCoin)
            CoinText.text = $"{this.coin}";
    }

    public void MinusCoin(int coin)
    {
        this.coin -= coin;
        if (isThereCoin)
            CoinText.text = $"{this.coin}";
    }
    #endregion

    #region Stage
    public void AcendStage()
    {
        StageText.text = $"{++stageFloor}";
        Debug.Log($"Stage: {stageFloor}");
    }

    public void ResetStageFloor()
    {
        stageFloor = 0;
    }
    #endregion

    #region Pause
    public void OnClickPauseButton()
    {
        Time.timeScale = 0;
        pauseHUD.SetActive(true);
    }

    public void OnClickBackButton()
    {
        Time.timeScale = 1;
        pauseHUD.SetActive(false);
    }

    public void OnClickExitButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("StartScene");
    }
    #endregion
}
