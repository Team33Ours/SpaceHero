using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 몬스터 스킬정보
/// </summary>
public class MonsterSkill : BaseSkill
{
    public MonsterSkill(string n, float m, float t, string d, float v1, float v2 = 0f, float v3 = 0f)
        : base(n,m,t,d,v1,v2,v3)
    {
    }
}
