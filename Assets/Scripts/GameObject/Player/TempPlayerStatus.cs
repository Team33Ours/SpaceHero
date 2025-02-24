using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStat", menuName = "custum/GameObject/Player", order = 1)]
public class TempPlayerStatus : ScriptableObject
{
    public float currentHP;
    public float maxHP;
    public float speed;
    public int attack;
    public int defence;
}