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

    // **Resources에서 기본 업적 JSON 파일 복사**
    private static List<Achievement> LoadDefaultAchievements()
    {
        string defaultFilePath = "Assets/Json/achievements.json";
        if (File.Exists(defaultFilePath))
        {
            // Assets 폴더에서 기본 JSON 파일 읽기
            string json = File.ReadAllText(defaultFilePath);
            AchievementListWrapper wrapper = JsonUtility.FromJson<AchievementListWrapper>(json);

            // 읽어온 데이터를 persistentDataPath에 저장
            File.WriteAllText(filePath, json);  // 업적 데이터를 저장
            Debug.Log("기본 업적 데이터 저장 완료!");

            return wrapper.achievements;
        }
        else
        {
            // 기본 업적 JSON을 찾을 수 없으면 경고 출력
            Debug.LogWarning("기본 업적 JSON을 찾을 수 없음!");
            return new List<Achievement>();  // 기본 업적이 없으면 빈 리스트 반환
        }
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