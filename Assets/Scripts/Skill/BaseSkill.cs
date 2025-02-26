using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;  // 역직렬화할때 반드시필요

/// <summary>
/// 플레이어와 몬스터의 스킬 정보
/// </summary>
[Serializable]
public class BaseSkill
{
    // 스킬에 들어갈 기본 정보
    // newtonsoft json은 private set을 사용할 수 없어서 private 제거
    [SerializeField] public string skillName { get; set; }

    // 스킬에 필요한 마나
    [SerializeField] public float skillMana { get; set; }

    // 스킬의 쿨타임
    [SerializeField] public float skillCoolTime { get; set; }

    // 스킬의 설명
    [SerializeField] public string skillDes { get; set; }

    // 스킬의 효과
    // float로 정한 값(자신의 체력,스피드, 몬스터의 체력)만큼 영향을 준다
    // newtonsoft json은 private set을 사용할 수 없어서 private 제거
    [SerializeField] public float value1 { get; set; }
    [SerializeField] public float value2 { get; set; }
    [SerializeField] public float value3 { get; set; }

    // 역직렬화에 사용할 기본 생성자 추가(기본 생성자가 public일때만 가능하다)
    public BaseSkill()
    {
    }

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
