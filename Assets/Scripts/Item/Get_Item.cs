using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Get_Item : MonoBehaviour
{
    // ������, ���̵�, ��ź ������Ʈ ����
    public GameObject itemGun;
    public GameObject itemRifle;
    public GameObject itemScythe;
    public GameObject itemBoom;

    private GameObject currentItem; // ���� ������ ������

    public void Start()
    {
        // ������ �� �⺻ ����� ��
        currentItem = itemGun;
        EquipItem(currentItem);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        // ������ �±� ������Ʈ�� �浹�Ѵٸ�
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
        // ���� �������� ��Ȱ��ȭ
        if (currentItem != null)
        {
            currentItem.SetActive(false);
        }
        
        // �� �������� Ȱ��ȭ
        currentItem = newItem;
        currentItem.SetActive(true);
    }

}
