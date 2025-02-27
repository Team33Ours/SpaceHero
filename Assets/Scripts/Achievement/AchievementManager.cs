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

    public void AddAchievement(Achievement newAchievement)
    {
        if (!achievements.Exists(a => a.id == newAchievement.id))
        {
            achievements.Add(newAchievement);
            SaveSystem.SaveAchievements(achievements);
        }
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
                Debug.Log($"업적 달성! {achievement.title}");
            }
            SaveSystem.SaveAchievements(achievements);

            AchievementUI.Instance.UpdateUI();  // UI 갱신
        }
    }

    public void ClaimReward(string id)
    {
        Achievement achievement = achievements.Find(a => a.id == id);

        if (achievement != null && achievement.isCompleted && !achievement.isRewarded)
        {
            achievement.isRewarded = true;
            Debug.Log($"{achievement.rewardAmount} 골드 지급!");

            // 골드 지급 코드 추가 (예: PlayerManager.Instance.AddGold(achievement.rewardAmount);)

            SaveSystem.SaveAchievements(achievements);
        }
    }

    public void LoadAchievements()
    {
        achievements = SaveSystem.LoadAchievements();
    }
}
