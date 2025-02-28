//using System.IO;
//using UnityEngine;
//using System.Collections.Generic;

//public static class SaveSystem
//{
//    private static string filePath = Application.persistentDataPath + "/achievements.json";

//    //  **업적 데이터 저장**
//    public static void SaveAchievements(List<Achievement> achievements)
//    {
//        string json = JsonUtility.ToJson(new AchievementListWrapper(achievements), true);
//        File.WriteAllText(filePath, json);
//        Debug.Log("업적 데이터 저장 완료!");
//    }

//    // **업적 데이터 불러오기 (없으면 기본 데이터 생성)**
//    public static List<Achievement> LoadAchievements()
//    {
//        if (File.Exists(filePath))
//        {
//            string json = File.ReadAllText(filePath);
//            AchievementListWrapper wrapper = JsonUtility.FromJson<AchievementListWrapper>(json);
//            Debug.Log("업적 데이터 불러오기 완료!");
//            return wrapper.achievements;
//        }
//        else
//        {
//            Debug.Log("저장된 업적 데이터 없음. 기본 데이터 로드 중...");
//            return LoadDefaultAchievements();
//        }
//    }

//    //  **Resources에서 기본 업적 JSON 파일 복사**
//    private static List<Achievement> LoadDefaultAchievements()
//    {
//        TextAsset defaultJson = Resources.Load<TextAsset>("achievements");
//        if (defaultJson != null)
//        {
//            AchievementListWrapper wrapper = JsonUtility.FromJson<AchievementListWrapper>(defaultJson.text);
//            File.WriteAllText(filePath, defaultJson.text);
//            Debug.Log("기본 업적 데이터 저장 완료!");
//            return wrapper.achievements;
//        }
//        else
//        {
//            Debug.LogWarning("기본 업적 JSON을 찾을 수 없음!");
//            return new List<Achievement>(); // 기본 업적도 없으면 빈 리스트 반환
//        }
//    }

//    // 📌 **업적 데이터 존재 여부 확인**
//    public static bool HasSavedData()
//    {
//        return File.Exists(filePath);
//    }
//}

//[System.Serializable]
//public class AchievementListWrapper
//{
//    public List<Achievement> achievements;

//    public AchievementListWrapper(List<Achievement> achievements)
//    {
//        this.achievements = achievements;
//    }
//}
using System.IO;
using UnityEngine;
using System.Collections.Generic;

public static class SaveSystem
{
    private static string filePath = Application.persistentDataPath + "/achievements.json";

    // **업적 데이터 저장**
    public static void SaveAchievements(List<Achievement> achievements)
    {
        string json = JsonUtility.ToJson(new AchievementListWrapper(achievements), true);
        File.WriteAllText(filePath, json);
        Debug.Log("업적 데이터 저장 완료!");
    }

    // **업적 데이터 불러오기 (없으면 기본 데이터 생성)**
    public static List<Achievement> LoadAchievements()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            AchievementListWrapper wrapper = JsonUtility.FromJson<AchievementListWrapper>(json);
            Debug.Log("업적 데이터 불러오기 완료!");
            return wrapper.achievements;
        }
        else
        {
            Debug.Log("저장된 업적 데이터 없음. 기본 데이터 로드 중...");
            return LoadDefaultAchievements();
        }
    }

    // **기본 업적 JSON 파일이 없으면 생성하고 저장**
    private static List<Achievement> LoadDefaultAchievements()
    {
        // 기본 업적 JSON 내용
        string defaultJson = @"
        {
            ""achievements"": [
                { ""id"": ""10001"", ""title"": ""몬스터 사냥꾼"", ""description"": ""몬스터 10마리 처치"", ""goalValue"": 10, ""currentValue"": 0, ""isCompleted"": false, ""rewardAmount"": 100, ""isRewarded"": false },
                { ""id"": ""10002"", ""title"": ""부품 수집가"", ""description"": ""부품 5개 획득"", ""goalValue"": 5, ""currentValue"": 0, ""isCompleted"": false, ""rewardAmount"": 50, ""isRewarded"": false },
                { ""id"": ""10003"", ""title"": ""레벨업"", ""description"": ""레벨 5 달성"", ""goalValue"": 5, ""currentValue"": 0, ""isCompleted"": false, ""rewardAmount"": 200, ""isRewarded"": false },
                { ""id"": ""10004"", ""title"": ""퀘스트 클리어"", ""description"": ""퀘스트 3개 클리어"", ""goalValue"": 3, ""currentValue"": 0, ""isCompleted"": false, ""rewardAmount"": 150, ""isRewarded"": false },
                { ""id"": ""10005"", ""title"": ""아이템 수집가"", ""description"": ""아이템 10개 획득"", ""goalValue"": 10, ""currentValue"": 0, ""isCompleted"": false, ""rewardAmount"": 100, ""isRewarded"": false },
                { ""id"": ""10006"", ""title"": ""장비 강화"", ""description"": ""장비 3번 강화"", ""goalValue"": 3, ""currentValue"": 0, ""isCompleted"": false, ""rewardAmount"": 150, ""isRewarded"": false },
                { ""id"": ""10007"", ""title"": ""친구 추가"", ""description"": ""친구 3명 추가"", ""goalValue"": 3, ""currentValue"": 0, ""isCompleted"": false, ""rewardAmount"": 100, ""isRewarded"": false },
                { ""id"": ""10008"", ""title"": ""친구와 함께"", ""description"": ""친구와 파티 3번"", ""goalValue"": 3, ""currentValue"": 0, ""isCompleted"": false, ""rewardAmount"": 150, ""isRewarded"": false },
                { ""id"": ""10009"", ""title"": ""길드 가입"", ""description"": ""길드 가입"", ""goalValue"": 1, ""currentValue"": 0, ""isCompleted"": false, ""rewardAmount"": 100, ""isRewarded"": false },
                { ""id"": ""10010"", ""title"": ""길드 활동"", ""description"": ""길드 활동 3번"", ""goalValue"": 3, ""currentValue"": 0, ""isCompleted"": false, ""rewardAmount"": 150, ""isRewarded"": false }
            ]
        }";

        // 기본 데이터를 파싱하여 리스트로 변환
        AchievementListWrapper wrapper = JsonUtility.FromJson<AchievementListWrapper>(defaultJson);

        // 기본 데이터를 persistentDataPath에 저장
        File.WriteAllText(filePath, defaultJson);
        Debug.Log("기본 업적 데이터 저장 완료!");

        return wrapper.achievements;
    }

    // 📌 **업적 데이터 존재 여부 확인**
    public static bool HasSavedData()
    {
        return File.Exists(filePath);
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