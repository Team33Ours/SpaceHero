using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStat", menuName = "custum/GameObject/Player", order = 1)]
public class Status : ScriptableObject
{
    public float maxHealth;
    public float speed;
    public float currentSpeed;
    public int attack;
    public int defence;
    public int EXP;
}