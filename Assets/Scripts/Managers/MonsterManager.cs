using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 몬스터의 생성, 사망처리
/// 2025.02.24.ImSeonggyun
/// </summary>
public class MonsterManager : MonoBehaviour
{
    // 몬스터의 경우 오브젝트 풀링을 적용
    public List<GameObject> monsterPool;    // 죽은 몬스터는 비활성화 후 Pool에 저장, List에서 제거
    public List<GameObject> monsterList;    // 활성화된 몬스터는 List에 저장, Pool에서 제거

    // 일반몬스터의 사망
    public void RemoveMonsterOnDeath(NormalMonsterController monster)
    {
        // 죽은 몬스터는 Pool에 넣고 List에서 제거 
        monster.gameObject.SetActive(false);
        

    }

}
