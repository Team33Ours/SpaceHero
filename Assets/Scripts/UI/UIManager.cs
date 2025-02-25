using TMPro;
using UnityEngine;
using UnityEngine.UI;

//코인
//    UIManager에서 인스턴스 만들어서 관리
//    코인 양 보여주는 TextMeshProUGUI 변수 생성
//    코인 늘어나는 메서드 AddCoin() 작성
//    코인 감소하는 메서드 MinusCoin() 작성
//스테이지
//    스테이지 보여주는 TextMeshProUGUI 변수 생성
//    레벨 생성자에서 스테이지 관리 쉽게끔 함수 작성
//    스테이지 늘어나는 메서드 ClearStage()
//퍼즈
//    버튼 달아서 게임 일시정지 함수(시간 스케일) 추가 후 인스펙터에서 할당
//    일시정지
//        게임 재개
//        메인메뉴

public class UIManager : Singleton<UIManager>
{
    public GameObject CoinUI;
    private TextMeshProUGUI CoinText;

    public GameObject StageUI;
    private int stageFloor; 
    private TextMeshProUGUI StageText;
    
    public GameObject PauseUI;

    private void Awake()
    {
        //CoinText = CoinUI.GetComponent<TextMeshProUGUI>();
        //StageText = StageUI.GetComponent<TextMeshProUGUI>();
        stageFloor = 0;
    }

    public void AcendStage()
    {
        stageFloor++;
        Debug.Log($"Stage: {stageFloor}");
        //StageText.text = $"{stageFloor}";
    }
}
