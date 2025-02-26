using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어와 몬스터의 스킬 정보
/// </summary>
[Serializable]
public class BaseSkill : MonoBehaviour
{
    // 스킬에 들어갈 기본 정보
    // 스킬의 이름
    [SerializeField] protected string skillName { get; private set; }

    // 스킬에 필요한 마나
    [SerializeField] protected float skillMana { get; private set; }

    // 스킬의 쿨타임
    [SerializeField] protected float skillCoolTime { get; private set; }

    // 스킬의 설명
    [SerializeField] protected string skillDes { get; private set; }

    // 스킬의 효과
    // float로 정한 값(자신의 체력,스피드, 몬스터의 체력)만큼 영향을 준다
    [SerializeField] protected float value1 { get; private set; }
    [SerializeField] protected float value2 { get; private set; }
    [SerializeField] protected float value3 { get; private set; }
}
