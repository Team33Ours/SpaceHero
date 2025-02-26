using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 몬스터의 생성, 사망처리
/// 2025.02.24.ImSeonggyun
/// </summary>
public class MonsterManager : Singleton<MonsterManager>
{
    // 몬스터의 경우 오브젝트 풀링을 적용
    public List<GameObject> monsterPool;    // 죽은 몬스터는 비활성화 후 Pool에 저장, List에서 제거
    public List<GameObject> monsterList;    // 활성화된 몬스터는 List에 저장, Pool에서 제거

    // Pool에서 몬스터가 있는지 검색
    public GameObject FindMonster()
    {
        // monsterPool에 있는 것 중 아무거나 1개 가져온다
        int rand = Random.Range(0, monsterPool.Count - 1);
        GameObject foundObj = monsterPool[rand];
        if (foundObj != null)
        {
            // 여기서는 monsterPool에서 제거만 한다
            // monsterList에 추가하는건 FindMonster를 호출하는 곳(CreateMonster)에서 한다
            foundObj.SetActive(true);
            monsterPool.Remove(foundObj);
            return foundObj;
        }
        return null;
    }

    // 일반몬스터가 생성될 때 호출
    public void CreateMonster()
    {
        // 먼저 pool을 찾아보고, 있으면 pool에 있는 것을 우선 가져온다
        GameObject enableObj = FindMonster();
        if (enableObj == null)
        {
            /// pool에 아무것도 없다면, 일반몬스터 중에서 아무거나 새로 생성한다
            //enableObj = GameObject.Instantiate();
            Debug.Log("enableObj가 null");
        }
        monsterList.Add(enableObj);

    }

    // 일반몬스터의 사망
    public void RemoveMonsterOnDeath(NormalMonsterController monster)
    {
        // 죽은 몬스터는 Pool에 넣고 List에서 제거 
        monster.gameObject.SetActive(false);
        monsterPool.Add(monster.gameObject);
        monsterList.Remove(monster.gameObject);
    }
}
