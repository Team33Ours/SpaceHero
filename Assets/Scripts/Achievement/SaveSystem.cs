using System.IO;
using UnityEngine;
using System.Collections.Generic;

public static class SaveSystem
{
    private static string filePath = Application.persistentDataPath + "/achievements.json";

    // **업적 데이터를 JSON 파일로 저장**
    public static void SaveAchievements(AchievementManager manager)
    {
        string json = JsonUtility.ToJson(new AchievementListWrapper(manager.achievements), true);
        File.WriteAllText(filePath, json);
        Debug.Log("업적 데이터 저장 완료!");
    }

    public static void LoadAchievementData(AchievementManager manager)
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            AchievementListWrapper wrapper = JsonUtility.FromJson<AchievementListWrapper>(json);
            manager.achievements = wrapper.achievements;
            Debug.Log("업적 데이터 불러오기 완료!");
        }
        else
        {
            Debug.Log("저장된 업적 데이터가 없음.");
        }
    }
}

[System.Serializable]
public class AchievementListWrapper
{
    public List<Achievement> achievements;

    public AchievementListWrapper(List<Achievement> achievements)
    {
        this.achievements = achievements;
    }
}
