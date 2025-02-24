using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������ ����, ���ó��
/// 2025.02.24.ImSeonggyun
/// </summary>
public class MonsterManager : Singleton<MonsterManager>
{
    // ������ ��� ������Ʈ Ǯ���� ����
    public List<GameObject> monsterPool;    // ���� ���ʹ� ��Ȱ��ȭ �� Pool�� ����, List���� ����
    public List<GameObject> monsterList;    // Ȱ��ȭ�� ���ʹ� List�� ����, Pool���� ����

    // Pool���� ���Ͱ� �ִ��� �˻�
    public GameObject FindMonster()
    {
        // monsterPool�� �ִ� �� �� �ƹ��ų� 1�� �����´�
        int rand = Random.Range(0, monsterPool.Count - 1);
        GameObject foundObj = monsterPool[rand];
        if (foundObj != null)
        {
            // ���⼭�� monsterPool���� ���Ÿ� �Ѵ�
            // monsterList�� �߰��ϴ°� FindMonster�� ȣ���ϴ� ��(CreateMonster)���� �Ѵ�
            foundObj.SetActive(true);
            monsterPool.Remove(foundObj);
            return foundObj;
        }
        return null;
    }

    // �Ϲݸ��Ͱ� ������ �� ȣ��
    public void CreateMonster()
    {
        // ���� pool�� ã�ƺ���, ������ pool�� �ִ� ���� �켱 �����´�
        GameObject enableObj = FindMonster();
        if (enableObj == null)
        {
            /// pool�� �ƹ��͵� ���ٸ�, �Ϲݸ��� �߿��� �ƹ��ų� ���� �����Ѵ�
            //enableObj = GameObject.Instantiate();
            Debug.Log("enableObj�� null");
        }
        monsterList.Add(enableObj);

    }

    // �Ϲݸ����� ���
    public void RemoveMonsterOnDeath(NormalMonsterController monster)
    {
        // ���� ���ʹ� Pool�� �ְ� List���� ���� 
        monster.gameObject.SetActive(false);
        monsterPool.Add(monster.gameObject);
        monsterList.Remove(monster.gameObject);
    }
}
