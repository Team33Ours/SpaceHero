using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Get_Item : MonoBehaviour
{
    // 라이플, 사이드, 폭탄 오브젝트 생성
    public GameObject itemGun;
    public GameObject itemRifle;
    public GameObject itemScythe;
    public GameObject itemBoom;

    private GameObject currentItem; // 현재 장착한 아이템

    public void Start()
    {
        // 시작할 때 기본 무기는 총
        currentItem = itemGun;
        EquipItem(currentItem);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        // 아이템 태그 오브젝트와 충돌한다면
        if(other.CompareTag("Item"))
        {
            string itemName = other.gameObject.name;

            if (itemName == "Rifle")
            {
                EquipItem(itemRifle);
            }
            else if (itemName == "Scythe")
            {
                EquipItem(itemScythe);
            }
            else if (itemName == "Boom")
            {
                EquipItem(itemBoom);
            }
            Destroy(other.gameObject);
        }
    }
    public void EquipItem(GameObject newItem)
    {
        // 기존 아이템을 비활성화
        if (currentItem != null)
        {
            currentItem.SetActive(false);
        }
        
        // 새 아이템을 활성화
        currentItem = newItem;
        currentItem.SetActive(true);
    }

}
