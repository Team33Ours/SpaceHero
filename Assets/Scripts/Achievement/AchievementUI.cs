using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class AchievementUI : MonoBehaviour
{
    public static AchievementUI Instance;


    public GameObject achievementItemPrefab; // 업적 프리팹
    public Transform contentPanel; // Scroll View 안의 Content


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        UpdateUI();
        
    }

    public void UpdateUI()
    {
        // 기존 UI 항목을 모두 삭제
        foreach (Transform child in contentPanel)
        {
            Destroy(child.gameObject);
        }


        // 업적 목록 업데이트
        foreach (Achievement achievement in AchievementManager.Instance.achievements)
        {
            GameObject newItem = Instantiate(achievementItemPrefab, contentPanel);

            // UI 요소 연결
            newItem.transform.Find("AchNameText").GetComponent<TextMeshProUGUI>().text = achievement.title;
            newItem.transform.Find("AchDecText").GetComponent<TextMeshProUGUI>().text = achievement.description;
            newItem.transform.Find("AchValueText").GetComponent<TextMeshProUGUI>().text = $"{achievement.currentValue} / {achievement.goalValue}";

            // 진행도 바 업데이트
            float progress = (float)achievement.currentValue / achievement.goalValue;
            newItem.transform.Find("AchProgressBar").GetComponent<Image>().fillAmount = 1 - progress;

            // 진행도가 0이 되면 AchProgressBar 비활성화
            if (progress >= 1)
            {
                newItem.transform.Find("AchProgressBar").gameObject.SetActive(false);
            }

            // 완료 여부 표시
            newItem.transform.Find("AchCompletedText").GetComponent<TextMeshProUGUI>().text = achievement.isCompleted ? "완료!" : "진행 중";

            // 보상 버튼 설정
            Button claimButton = newItem.transform.Find("AchRewardBtn").GetComponent<Button>();
            claimButton.interactable = achievement.isCompleted && !achievement.isRewarded;

            // 클릭 시 업적 ID를 전달하도록 람다 함수 사용
            string achievementId = achievement.id; // 지역 변수로 ID 저장
            claimButton.onClick.RemoveAllListeners(); // 기존 리스너 제거 (중복 방지)
            claimButton.onClick.AddListener(() =>
            {
                AchievementManager.Instance.ClaimReward(achievementId);  // 보상 지급
                UpdateUI();  // 보상 지급 후 UI 갱신
            });
        }
    }
}
