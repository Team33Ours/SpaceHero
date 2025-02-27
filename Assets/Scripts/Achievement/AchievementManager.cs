using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    public static AchievementManager Instance;

    public List<Achievement> achievements = new List<Achievement>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        LoadAchievements();
    }

    public void AddAchievement(Achievement achievement)
    {
        achievements.Add(achievement);
        AchievementUI.Instance.UpdateUI();  // 업적 추가 후 UI 갱신

    }

    public void CheckAchievementProgress(string id, int amount)
    {
        Achievement achievement = achievements.Find(a => a.id == id);

        if (achievement != null && !achievement.isCompleted)
        {
            achievement.currentValue += amount;
            if (achievement.currentValue >= achievement.goalValue)
            {
                achievement.currentValue = achievement.goalValue;
                achievement.isCompleted = true;
                GrantReward(achievement);
                Debug.Log($"업적 달성! {achievement.title}");
            }
        }
        // 업적 진행 상태가 변경되었으므로 UI 갱신
        AchievementUI.Instance.UpdateUI();
    }

    private void GrantReward(Achievement achievement)
    {
        Debug.Log($"{achievement.rewardAmount} 골드 지급!");
    }

    public void SaveAchievements()
    {
        SaveSystem.SaveAchievements(this);
    }

    public void LoadAchievements()
    {
        SaveSystem.LoadAchievementData(this);
    }
}
