using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �÷��̾�, ������ ��ų�� �����ϰ� ����ϴ� ���
/// 2025.02.25.ImSeonggyun
/// </summary>
public class BaseSkillHandler : MonoBehaviour
{
    // �÷��̾�� ���Ͱ� �������� �������� ��
    // ��ų�� ������ �����̳�
    protected Dictionary<string, BaseSkill> skillDictionary;   // ��ų�� ����

    // ����� ��ų�� ������ delegate �Ǵ� action

    public void Awake()
    {
        skillDictionary = new Dictionary<string, BaseSkill>();

    }


    // ��ų�� �߰�, ���ű��(delegate �Ǵ�, Action�� �߰��ؼ� ���)




}
