using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �÷��̾�� ���� ��ų�� json���� �����ϰ� �ҷ��´�
/// 2025.02.25.ImSeonggyun
/// </summary>
public class DataManager : Singleton<DataManager>
{
    // ��ų�� �̸��� �����Ѵ�
    public Dictionary<string, BaseSkill> playerSkills;
    public Dictionary<string, BaseSkill> playerLearned;
    public Dictionary<string, BaseSkill> bossMobSkills;
    

    private void Awake()
    {
        // ����� �ʱ�ȭ�� Awake

    }


}
