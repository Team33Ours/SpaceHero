using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어, 몬스터가 스킬을 호출할 때 delegate에 추가
/// 2025.02.25.ImSeonggyun
/// </summary>
public class BaseSkillHandler : MonoBehaviour
{
    // 플레이어와 몬스터가 공용으로 가질만한 것
    // 스킬을 저장할 컨테이너
    protected Dictionary<string, BaseSkill> skillDictionary;   // 스킬을 저장

    // 사용할 스킬을 저장할 delegate 또는 action

    public void Awake()
    {
        skillDictionary = new Dictionary<string, BaseSkill>();

    }


    // 스킬의 추가, 제거기능(delegate 또는, Action에 추가해서 사용)




}
