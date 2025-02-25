using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어와 몬스터의 스킬 정보
/// </summary>
public class BaseSkill : MonoBehaviour
{
    // 스킬에 들어갈 기본 정보
    // 스킬의 이름
    protected string skillName { get; private set; }

    // 스킬에 필요한 마나
    protected float skillMana { get; private set; }

    // 스킬의 쿨타임
    protected float skillCoolTime { get; private set; }
    
    // ��ų�� ����
    protected string skillDes { get; private set; }

    // ��ų�� ȿ��(����, ���ݼӵ����� �����͵�)
    


}
