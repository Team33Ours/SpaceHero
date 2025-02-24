using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������ ����, ���ó��
/// 2025.02.24.ImSeonggyun
/// </summary>
public class MonsterManager : MonoBehaviour
{
    // ������ ��� ������Ʈ Ǯ���� ����
    public List<GameObject> monsterPool;    // ���� ���ʹ� ��Ȱ��ȭ �� Pool�� ����, List���� ����
    public List<GameObject> monsterList;    // Ȱ��ȭ�� ���ʹ� List�� ����, Pool���� ����

    // �Ϲݸ����� ���
    public void RemoveMonsterOnDeath(NormalMonsterController monster)
    {
        // ���� ���ʹ� Pool�� �ְ� List���� ���� 
        monster.gameObject.SetActive(false);
        

    }

}
