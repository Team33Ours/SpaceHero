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

     // 체력 회복 아이템
     public GameObject itemHealthPotion;
     public GameObject itemHealthPack;
     private GameObject currentItem; // 현재 장착한 아이템
    
    public Stat_Item gunStat;    // 기본 무기 능력치
    public Stat_Item rifleStat;  // 라이플 아이템 능력치
    public Stat_Item scytheStat; // 낫 아이템 능력치
    public Stat_Item boomStat;   // 폭탄 아이템 능력치
    public Stat_Item healthPotionStat; // 체력 포션 능력치
    public Stat_Item healthPackStat;   // 체력 팩 능력치
    
    public void Start()
    {
        // 시작할 때 기본 무기는 총
         //currentItem = itemGun;
        // EquipItem(currentItem);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        // 아이템 태그 오브젝트와 충돌한다면
        if (other.CompareTag("Item"))
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
            else if (itemName == "HealthPotion" || itemName == "HealthPack")

                // 체력 회복 아이템을 먹었을 때 회복하고 사라짐
                RecoveryHealth(other.gameObject);
            Destroy(other.gameObject);
            Debug.Log(itemName);
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
    public void RecoveryHealth(GameObject item)
    {
        // 체력 회복 아이템을 먹으면 아이템을 사라지게 함
        Destroy(item);
    }
}

