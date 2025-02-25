using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어와 몬스터의 체력,마력,스피드
/// 2025.02.25.ImSeonggyun
/// </summary>
[Serializable]
public class StatHandler : MonoBehaviour
{
    [SerializeField] private float maxHealth;   
    [SerializeField] private float maxMana;    
    [SerializeField] private float maxSpeed;      
    public float MaxHealth
    {
        get => maxHealth;
        set => maxHealth = Mathf.Max(1, value); 
    }
    public float MaxMana
    {
        get => maxMana;
        set => maxMana = Mathf.Max(0, value);
    }
    public float MaxSpeed
    {
        get => maxSpeed;
        set => maxSpeed = Mathf.Max(1, value);
    }
}
