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

    // 사용할 스킬을 저장할 delegate 또는 action


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
        // 데이터매니저로부터 스킬을 가져온다
        LoadAllSkills();
    }

    // 데이터매니저에서 가져온다
    public void LoadAllSkills()
    {

    }


    // 플레이어, 몬스터가 스킬을 호출할 때 delegate에 추가하고 삭제

}
