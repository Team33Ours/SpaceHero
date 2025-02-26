using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어와 몬스터의 스킬 정보
/// </summary>
[Serializable]
public class BaseSkill
{
    // 스킬에 들어갈 기본 정보
    // 스킬의 이름
    [SerializeField] public string skillName { get; private set; }

    // 스킬에 필요한 마나
    [SerializeField] public float skillMana { get; private set; }

    // 스킬의 쿨타임
    [SerializeField] public float skillCoolTime { get; private set; }

    // 스킬의 설명
    [SerializeField] public string skillDes { get; private set; }

    // 스킬의 효과
    // float로 정한 값(자신의 체력,스피드, 몬스터의 체력)만큼 영향을 준다
    [SerializeField] public float value1 { get; private set; }
    [SerializeField] public float value2 { get; private set; }
    [SerializeField] public float value3 { get; private set; }

    protected BaseSkill(string n, float m, float t, string d, float v1, float v2 = 0f, float v3 = 0f)
    {
        skillName = n;
        skillMana = m;
        skillCoolTime = t;
        skillDes = d;
        value1 = v1;
        value2 = v2;
        value3 = v3;
    }
    public string GetName()
    {
        return skillName;
    }

}
