using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop_Item : MonoBehaviour
{
    // 드랍할 아이템 목록
    public GameObject[] dropItems;

    // 아이템 드랍 확률
    [Range(0f, 1f)] public float dropChance = 0.3f;

    public void DropItem()
    {
        // 확률 계산 (0 ~ 1 사이의 랜덤 값 생성)
        if (Random.value <= dropChance)
        {
            // 아이템을 드랍할 위치 (몬스터의 현재 위치)
            Vector3 dropPosition = transform.position;

            // 드랍할 아이템 랜덤 선택
            int randomIndex = Random.Range(0, dropItems.Length);
            GameObject selectedItem = dropItems[randomIndex];

            // 아이템 생성
            Instantiate(selectedItem, dropPosition, Quaternion.identity);
        }
    }
}

