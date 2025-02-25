using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어와 몬스터 스킬을 json으로 저장하고 불러온다
/// 2025.02.25.ImSeonggyun
/// </summary>
public class DataManager : Singleton<DataManager>
{
    // 스킬은 이름만 저장한다
    public Dictionary<string, BaseSkill> playerSkills;
    public Dictionary<string, BaseSkill> playerLearned;
    public Dictionary<string, BaseSkill> bossMobSkills;
    

    private void Awake()
    {
        // 멤버의 초기화는 Awake

    }


}
