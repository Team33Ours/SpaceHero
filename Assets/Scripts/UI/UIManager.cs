using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;
using Unity.VisualScripting;
using System.Linq;
using System;
using System.Collections;

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
    TempSkill[] allOfTempSkills; // Resources의 모든 스킬을 읽는다.
    TempSkill[] get3RandomSkills = new TempSkill[3];
    public Image[] rouletteImages = new Image[3];
    internal int moveCount;
    internal System.Random setMoveTimes;

    // Test
    public float duration = 0.8f;

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

        //
        setMoveTimes = new System.Random();
        moveCount = setMoveTimes.Next(5, 8);
        Debug.Log($"반복 횟수: {moveCount}회");
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
        Time.timeScale = 0;

        // Resources/Skills 폴더에 있는 모든 TempSkill 타입 에셋을 읽음
        allOfTempSkills = Resources.LoadAll<TempSkill>("Skills");


        StartCoroutine(RunRouletteSequence());

        //for (int j = 0; j < 3; j++)
        //{
        //    rouletteImages[j].gameObject.transform.localPosition = new Vector3(rouletteImages[j].gameObject.transform.localPosition.x, 400, rouletteImages[j].gameObject.transform.localPosition.z);
        //}
    }

    private IEnumerator RunRouletteSequence()
    {
        for (int i = 0; i < moveCount; i++)
        {
            // 1회 애니메이션 실행 및 완료 대기
            yield return StartCoroutine(ImagesMoveRefeatCoroutine());

            // 이미지 위치 초기화
            ResetImageTransform();

            Debug.Log($"OnComplete: {i + 1}회 완료");
        }
        Debug.Log("룰렛 애니메이션 완료");

        ResetImageTransform();
        get3RandomSkills = Set3RandomSkills();
        SetRouletteImages();

        var tween1 = rouletteImages[0].transform.DOLocalMoveY(rouletteImages[0].transform.localPosition.y - 400f, duration).SetUpdate(true).SetEase(Ease.OutBounce);
        var tween2 = rouletteImages[1].transform.DOLocalMoveY(rouletteImages[1].transform.localPosition.y - 400f, duration).SetUpdate(true).SetEase(Ease.OutBounce);
        var tween3 = rouletteImages[2].transform.DOLocalMoveY(rouletteImages[2].transform.localPosition.y - 400f, duration).SetUpdate(true).SetEase(Ease.OutBounce);

        Time.timeScale = 1; // 타임스케일 복원
    }
    private IEnumerator ImagesMoveRefeatCoroutine()
    {
        // 스킬 3개를 뽑음
        get3RandomSkills = Set3RandomSkills();
        // 뽑은 스킬을 이미지 UI에 적용
        SetRouletteImages();

        // 모든 애니메이션 동시 시작
        var tween1 = rouletteImages[0].transform.DOLocalMoveY(rouletteImages[0].transform.localPosition.y - 800f, duration).SetUpdate(true).SetEase(Ease.OutCubic);
        var tween2 = rouletteImages[1].transform.DOLocalMoveY(rouletteImages[1].transform.localPosition.y - 800f, duration).SetUpdate(true).SetEase(Ease.OutCubic);
        var tween3 = rouletteImages[2].transform.DOLocalMoveY(rouletteImages[2].transform.localPosition.y - 800f, duration).SetUpdate(true).SetEase(Ease.OutCubic);

        // 마지막 애니메이션 완료 대기
        yield return tween3.WaitForCompletion();
    }

    private void ResetImageTransform()
    {
        for (int j = 0; j < 3; j++)
        {
            rouletteImages[j].transform.localPosition = new Vector3(
                rouletteImages[j].transform.localPosition.x,
                400,
                rouletteImages[j].transform.localPosition.z
            );
        }
    }


    private TempSkill[] Set3RandomSkills()
    {
        System.Random rng = new System.Random();
        allOfTempSkills = allOfTempSkills.OrderBy(s => rng.Next()).ToArray(); // OrderBy와 Random을 사용하여 랜덤으로 섞은 배열을 만듦
        
        for (int i = 0; i < 3; i++)
        {
            get3RandomSkills[i] = allOfTempSkills[i];
        }

        return get3RandomSkills;
    }

    private void SetRouletteImages()
    {
        // 스프라이트 적용
        rouletteImages[0].sprite = get3RandomSkills[0].Icon;
        rouletteImages[1].sprite = get3RandomSkills[1].Icon;
        rouletteImages[2].sprite = get3RandomSkills[2].Icon;

        // 텍스트에 설명 적용
        rouletteImages[0].GetComponentInChildren<TextMeshProUGUI>(true).text = get3RandomSkills[0].Name;
        rouletteImages[1].GetComponentInChildren<TextMeshProUGUI>(true).text = get3RandomSkills[1].Name;
        rouletteImages[2].GetComponentInChildren<TextMeshProUGUI>(true).text = get3RandomSkills[2].Name;
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
