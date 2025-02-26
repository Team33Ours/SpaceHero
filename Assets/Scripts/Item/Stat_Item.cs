using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item/Stat Item", order = 1)]
public class Stat_Item : ScriptableObject
{
    public string itemName; // 아이템 이름
    public int atkPower; // 무기 공격력
    public float atkSpeed; // 무기 공격속도
    public int healthAmount; // 체력 회복량
    
}
