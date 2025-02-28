using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using System.Linq;
using System.Collections;
using Coffee.UIExtensions;

public class UIManager : Singleton<UIManager>
{
    [Header("Roulette")]
    public GameObject roletteCanvas;
    PlayerSkill[] allOfTempSkills; // Resources의 모든 스킬을 읽는다.
    PlayerSkill[] get3RandomSkills = new PlayerSkill[3];
    PlayerSkill Skill1st, Skill2nd, Skill3rd;
    public UIParticle[] UIParticles = new UIParticle[3]; // Rairty에 따라 다른 파티클을 적용
    public Image[] rouletteImages = new Image[3];
    private UpStatusFromSkill player;
    public float duration = 1f;
    internal System.Random setRefeatTimes;
    internal int moveCount;
    internal int EXP;

    [Header("Coin")]
    [SerializeField]
    private TextMeshProUGUI CoinText;
    private int coin;
    private int coinDifferenceCheck;
    bool isThereCoin;

    [Header("Stage")]
    private int stageFloor;
    public TextMeshProUGUI StageText;

    [Header("Pause")]
    public GameObject pauseHUD;
    public Button PauseUI;

    [Header("EXP")]
    [SerializeField]
    private Slider EXPSlider;

    protected override void Awake()
    {
        base.Awake();

        // Roulette
        player = GameObject.FindWithTag("Player").GetComponent<UpStatusFromSkill>();
        setRefeatTimes = new System.Random();

        // Level
        EXP = 0;

        // Coin
        coin = 0;
        CoinText = GameObject.FindWithTag("Coin").GetComponent<TextMeshProUGUI>();
        CoinText.text = $"{coin}";
        isThereCoin = (CoinText != null) ? true : false;

        // Stage
        stageFloor = 1;
        StageText.text = $"{stageFloor}";

        // Pause
        PauseUI = GetComponent<Button>();

        // EXP
        EXPSlider = GetComponentInChildren<Slider>();
        EXPSlider.maxValue = 150;
        EXPSlider.value = 0;
    }

    private void Update()
    {
        EXPSlider.value = EXP;
    }

    public void LevelUP()
    {
        EXP -= 150;
        RunRoulette();
        if (EXP >= 150)
            LevelUP();
    }

    #region Roulette
    private void RunRoulette()
    {
        Time.timeScale = 0;
        roletteCanvas.SetActive(true);
        moveCount = setRefeatTimes.Next(2, 4);

        // Resources/Skills 폴더에 있는 모든 TempSkill 타입 에셋을 읽음
        allOfTempSkills = Resources.LoadAll<PlayerSkill>("Skills");

        // 룰렛 애니메이션을 반복 실행하기 위해 2개의 코루틴을 만듦
        StartCoroutine(RunRouletteSequence());
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

        // 스킬 3개를 뽑음
        get3RandomSkills = Set3RandomSkills();

        // 뽑은 스킬을 이미지 UI에 적용
        SetRouletteImages();

        rouletteImages[0].transform.DOLocalMoveY(rouletteImages[0].transform.localPosition.y - 400f, duration).SetUpdate(true).SetEase(Ease.OutBounce);
        rouletteImages[1].transform.DOLocalMoveY(rouletteImages[1].transform.localPosition.y - 400f, duration).SetUpdate(true).SetEase(Ease.OutBounce);
        rouletteImages[2].transform.DOLocalMoveY(rouletteImages[2].transform.localPosition.y - 400f, duration).SetUpdate(true).SetEase(Ease.OutBounce);
    }
    private IEnumerator ImagesMoveRefeatCoroutine()
    {
        get3RandomSkills = Set3RandomSkills();
        SetRouletteImages();

        // 모든 애니메이션 동시 시작
        var tween1 = rouletteImages[0].transform.DOLocalMoveY(rouletteImages[0].transform.localPosition.y - 800f, duration).SetUpdate(true).SetEase(Ease.OutCubic);
        var tween2 = rouletteImages[1].transform.DOLocalMoveY(rouletteImages[1].transform.localPosition.y - 800f, duration).SetUpdate(true).SetEase(Ease.OutCubic);
        var tween3 = rouletteImages[2].transform.DOLocalMoveY(rouletteImages[2].transform.localPosition.y - 800f, duration).SetUpdate(true).SetEase(Ease.OutCubic);

        // 마지막 애니메이션 완료 대기
        yield return tween3.WaitForCompletion();
    }

    private PlayerSkill[] Set3RandomSkills()
    {
        System.Random rng = new System.Random();
        allOfTempSkills = allOfTempSkills.OrderBy(s => rng.Next()).ToArray(); // OrderBy와 Random을 사용하여 랜덤으로 섞은 배열을 만듦

        for (int i = 0; i < 3; i++)
        {
            get3RandomSkills[i] = allOfTempSkills[i];
        }

        Skill1st = get3RandomSkills[0];
        Skill2nd = get3RandomSkills[1];
        Skill3rd = get3RandomSkills[2];

        return get3RandomSkills;
    }

    private void SetRouletteImages()
    {
        for (int i = 0; i < 3; i++)
        {
            // 스프라이트 적용
            rouletteImages[i].sprite = get3RandomSkills[i].Icon;

            // 텍스트 이름 적용
            rouletteImages[i].GetComponentInChildren<TextMeshProUGUI>(true).text = get3RandomSkills[i].Name;

            // 파티클 적용
            if (get3RandomSkills[i].particles != null)
            {
                UIParticles[i].gameObject.SetActive(true);
                GameObject particle = get3RandomSkills[i].particles;
                // 게임오브젝트 파티클 프리팹을 매개로 주면 UI에 표시되게 해줌
                UIParticles[i].SetParticleSystemPrefab(particle);
                particle.GetComponent<ParticleSystem>().Play();
            }
            else if (get3RandomSkills[i].particles == null)
            {
                UIParticles[i].gameObject.SetActive(false);
            }
        }
    }

    private void ResetImageTransform()
    {
        for (int j = 0; j < 3; j++)
        {
            rouletteImages[j].transform.localPosition = new Vector3(
                rouletteImages[j].transform.localPosition.x, 400, rouletteImages[j].transform.localPosition.z
            );
        }
    }
    #endregion

    #region RouletteBtn
    public void OnClickSkill1stSkill()
    {
        player.GetSkill(Skill1st);
        roletteCanvas.SetActive(false);
        Time.timeScale = 1;
    }

    public void OnClickSkill2ndSkill()
    {
        player.GetSkill(Skill2nd);
        roletteCanvas.SetActive(false);
        Time.timeScale = 1;
    }

    public void OnClickSkill3rdSkill()
    {
        player.GetSkill(Skill3rd);
        roletteCanvas.SetActive(false);
        Time.timeScale = 1;
    }
    #endregion

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
        
        SoundManager.Instance.ChangeBackGroundMusic(SoundManager.Instance.backgroundMusic[0]);
        Time.timeScale = 1;
        SceneManager.LoadScene("StartScene");
        pauseHUD.SetActive(false);
        ResetStageFloor();
        // 코인 저장하려면 따로 추가
        Destroy(this.gameObject);
    }
    #endregion

}
