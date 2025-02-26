using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;
using Unity.VisualScripting;

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

    // Roulette
    TempSkill[] allOfTempskills; // Resources의 모든 스킬을 읽는다.

    // Test
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
        RunRoulette();
    }

    public void RunRoulette()
    {
        /*
         * 구조
         * 1. 레벨업을 하면 패널이 나타나고 룰렛이 돌아간다
         * 2. 룰렛이 돌아가는 동안 Time.timeScale = 0으로 맞춘다.
         * 3. 룰렛이 돌아가는 동안 UI 애니메이션이 돌아갈 수 있도록 DOTween을 사용한다.
         * 
         * 룰렛 돌리기
         * 1. TempSkill의 Scriptable Object를 읽어서 배열에 넣는다.
         * 2. 3개의 랜덤한 스킬을 뽑는다.
         * 3. 해당 스킬의 Scriptable Object의 Sprite데이터를 읽어서 이미지에 적용한다.
         * 4. DOTween 기능으로 y축 아래로 움직인다.
         * 5. 일정 아래로 움직이면 새롭게 Scriptable Object의 데이터를 읽는다.
         * 6. 5~6초간 반복한다.
         * 7. 멈추면 3개 중 하나의 스킬을 선택한다.
         * 8. 선택한 스킬을 적용한다.
         * 9. Time.timeScale = 1로 맞춘다.
         */
        // Resources/Skills 폴더에 있는 모든 TempSkill 타입 에셋을 읽음
        allOfTempskills = Resources.LoadAll<TempSkill>("Skills");

        if (allOfTempskills != null)
        {
            Debug.Log("tempSkills is not null");
            foreach (var data in allOfTempskills)
            {
                Debug.Log($"Skill Name: {data.Name}");
            }
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
        pauseHUD.SetActive(false);
        ResetStageFloor();
        // 코인 저장하려면 따로 추가
        Destroy(this.gameObject);
    }
    #endregion
}
