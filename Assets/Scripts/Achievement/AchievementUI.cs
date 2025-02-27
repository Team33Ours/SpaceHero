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
            newItem.transform.Find("AchProgressBar").GetComponent<Image>().fillAmount = progress;

            // 완료 여부 표시
            newItem.transform.Find("AchCompletedText").GetComponent<TextMeshProUGUI>().text = achievement.isCompleted ? "완료!" : "진행 중";
        }
    }
}
