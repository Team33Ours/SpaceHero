using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어와 몬스터의 스킬을 관리하는 매니저
/// 
/// </summary>
public class SkillManager : Singleton<SkillManager>
{
    // 스킬은 이름만 저장한다
    public Dictionary<string, BaseSkill> playerSkills;  // 플레이어 스킬
    public Dictionary<string, BaseSkill> playerLearned; // 플레이어가 배운 스킬
    public Dictionary<string, BaseSkill> bossMobSkills; // 보스몬스터 스킬

    DataManager dataManager;

    private void Awake()
    {


        // 멤버의 초기화는 Awake
        if (playerSkills == null)
            playerSkills = new Dictionary<string, BaseSkill>();
        if (playerLearned == null)
            playerLearned = new Dictionary<string, BaseSkill>();
        if (bossMobSkills == null)
            bossMobSkills = new Dictionary<string, BaseSkill>();

        dataManager = DataManager.Instance;

    }
    public void Start()
    {
        // 임시
        SaveDefaultSkills();


        // 데이터매니저로부터 스킬을 가져온다
        LoadAllSkills();
    }

    public void SaveDefaultSkills()
    {
        // 플레이어의 전체 스킬




        // 몬스터의 전체 스킬
        MonsterSkill s1 = new MonsterSkill("IceBall", 0f, 1f, "1초에 한번씩 날라오는 아이스볼. 맞으면 5초동안 느려진다", 10f, 2f, 5f); // 데미지: 10f, 느려짐: 2f만큼, 지속시간: 5f
        MonsterSkill s2 = new MonsterSkill("ThunderTackle", 0f, 10f, "5초에 한번씩 온몸에 전기를 두르고 달려든다. 데미지가 매우 세다", 30f);    // 데미지: 30f
        bossMobSkills.Add(s1.GetName(), s1);
        bossMobSkills.Add(s2.GetName(), s2);
        // 임시 json 파일 만든다
        dataManager.SaveAllMonsterSkills();
    }

    // 데이터매니저에서 가져온다
    public void LoadAllSkills()
    {
        // json파일로부터 플레이어, 몬스터 스킬을 불러온다
        // 플레이어



        // 몬스터
        dataManager.LoadAllMonsterSkills();
    }

    // 플레이어, 몬스터가 스킬을 호출할 때 delegate에 추가하고 삭제하는 것은 여기서 하지 않고, PlayerController와 BossMonsterController에서 한다
}
