using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// 플레이어와 몬스터 스킬을 json으로 저장하고 불러온다
/// 2025.02.25.ImSeonggyun
/// </summary>
public class DataManager : Singleton<DataManager>
{
    // 스킬은 이름만 저장한다
    public List<BaseSkill> playerSkills;  // 플레이어 스킬
    public List<BaseSkill> playerLearned; // 플레이어가 배운 스킬
    public List<BaseSkill> bossMobSkills; // 보스몬스터 스킬

    public SkillManager skillManager;

    /// <summary>
    /// 씬이 로드될 때 OnSceneLoaded 추가
    /// </summary>
    //protected override void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    //{
    //    base.OnSceneLoaded(scene, mode);  // 부모 클래스 로그 출력 유지

    //    // 씬 변경될 때 실행할 코드 추가

    //    // 초기화 코드 
    //    if (playerSkills == null)
    //        playerSkills = new List<BaseSkill>();
    //    if (playerLearned == null)
    //        playerLearned = new List<BaseSkill>();
    //    if (bossMobSkills == null)
    //        bossMobSkills = new List<BaseSkill>();


    //    // SkillManager가 null이면 다시 가져오기(SkillManager가 DataManager보다 먼저 생성)
    //    if (skillManager == null)
    //    {
    //        skillManager = SkillManager.Instance;
    //    }
    //}
    ///// <summary>
    ///// 씬이 언로드될때 OnDestroy 추가
    ///// </summary>
    //protected override void OnDestroy()
    //{
    //    base.OnDestroy();
    //}

    private void Awake()
    {
        //// 멤버의 초기화는 Awake
        //if (playerSkills == null)
        //    playerSkills = new List<BaseSkill>();
        //if (playerLearned == null)
        //    playerLearned = new List<BaseSkill>();
        //if (bossMobSkills == null)
        //    bossMobSkills = new List<BaseSkill>();
    }
    public void Start()
    {
        //skillManager = SkillManager.Instance;
    }

    public string GetFilePath(string filename, bool isSaveFile = false)
    {
        // 저장폴더 설정(세이브파일인 경우와 아닌 경우 폴더를 구분해 저장한다)
        string directoryPath = Application.dataPath + (isSaveFile ? "/Json/GameDatas/" : "/Json/GameDatas/");
        // 폴더가 없으면 생성
        if (!Directory.Exists(directoryPath))
            Directory.CreateDirectory(directoryPath);
        return Path.Combine(directoryPath, filename);
    }
    // playerSkills와 bossMobSkills에 있는 모든 스킬을 각각 json파일에 저장
    public void SaveAllPlayerSkills()
    {
        playerSkills = skillManager.playerSkills.Values.ToList();
        string filePath = GetFilePath("AllPlayerSkillDatas.json", false);
        // json 데이터 변환
        string jsonData = JsonConvert.SerializeObject(playerSkills);
        // 파일 저장
        File.WriteAllText(filePath, jsonData);
        Debug.Log("플레이어 전체 스킬 데이터 저장 완료"); // debug
    }
    public void SaveAllMonsterSkills()
    {
        bossMobSkills = skillManager.bossMobSkills.Values.ToList();
        string filePath = GetFilePath("AllMonsterSkillDatas.json", false);
        string jsonData = JsonConvert.SerializeObject(bossMobSkills);
        File.WriteAllText(filePath, jsonData);
        Debug.Log("몬스터 전체 스킬 데이터 저장 완료"); // debug
    }
    // 게임이 시작하자마자 전체 스킬파일을 불러온 다음 playerSkills와 bossMobSkills에 할당
    public void LoadAllPlayerSkills()
    {
        // 불러올 파일의 경로
        string filePath = GetFilePath("AllPlayerSkillDatas.json", false);
        // 읽은 정보는 playerSkills에 저장
        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            /// List<BaseSkill>을 잘 불러오는지 확인해봐야한다
            playerSkills = JsonConvert.DeserializeObject<List<BaseSkill>>(jsonData);
            Debug.Log("플레이어 전체 스킬 데이터 불러오기 완료"); // debug
        }
    }
    public void LoadAllMonsterSkills()
    {
        string filePath = GetFilePath("AllMonsterSkillDatas.json", false);
        // 읽은 정보는 playerSkills에 저장
        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            /// List<BaseSkill>을 잘 불러오는지 확인해봐야한다
            bossMobSkills = JsonConvert.DeserializeObject<List<BaseSkill>>(jsonData);   // 
            if (bossMobSkills != null)
            {
                skillManager.bossMobSkills.Clear();

                foreach (BaseSkill mobSkill in bossMobSkills)
                {
                    skillManager.bossMobSkills[mobSkill.skillName] = mobSkill;
                }
            }
            Debug.Log($"몬스터 전체 스킬 데이터 불러오기 완료. {bossMobSkills.Count}개의 스킬 로드됨."); // debug        }
        }
    }
    // 그다음, 플레이어의 스킬을 저장할때는 string만 저장하고
    // 불러온 name을 키 값으로 하여 playerSkills에 있는 것을 찾아서 playerLearned에 추가한다
    public void SavePlayerLearnedSkills()
    {
        // 스킬의 이름만 저장한다
        List<string> skillNames = new List<string>();
        if (playerLearned.Count > 0)
        {
            foreach (BaseSkill skill in playerLearned)
            {
                skillNames.Add(skill.GetName());
            }
            string filePath = GetFilePath("LearnedSkills", true);
            string jsonData = JsonConvert.SerializeObject(skillNames);
            File.WriteAllText(filePath, jsonData);
        }
        Debug.Log("플레이어가 익힌 스킬 데이터 저장 완료"); // debug
    }

    // 불러오기
    public void LoadPlayerLearnedSkills()
    {
        // 스킬의 이름만 불러오고, playerSkills에서 검색한다
        string filePath = GetFilePath("LearnedSkills", true);
        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            /// List<BaseSkill>을 잘 불러오는지 확인해봐야한다
            playerLearned = JsonConvert.DeserializeObject<List<BaseSkill>>(jsonData);
            // 이걸 하나씩 SkillManager에 대입해야한다

        }
        Debug.Log("플레이어가 익힌 스킬 데이터 불러오기 완료"); // debug
    }
}
