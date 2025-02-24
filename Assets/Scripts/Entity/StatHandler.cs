using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어와 몬스터의 최대 체력,최대 마력,속도
/// 결합도를 낮추기 위해
/// 실시간 체력,마력은 ResourceController로 옮겼다
/// 2025.02.24.ImSeonggyun
/// </summary>
public class StatHandler : MonoBehaviour
{
    // 체력은 사용하고 싶은 클래스에서 할당한다
    [SerializeField] private float maxHealth;    // 최대체력 
    //[SerializeField] private float health;       // 실시간 체력
    [SerializeField] private float maxMana;    // 최대마나
    //[SerializeField] private float mana;       // 실시간 마나
    [SerializeField] private float maxSpeed;       // 스피드
    // 플레이어, 몬스터 생성할때 할당한다
    public float MaxHealth
    {
        get => maxHealth;
        set => maxHealth = Mathf.Max(1, value); // 최소 1 이상으로 설정
    }
    //public float Health
    //{
    //    get => health;
    //    set => health = Mathf.Max(0, value);    // 최소 0 이상으로 설정
    //}
    public float MaxMana
    {
        get => maxMana;
        set => maxMana = Mathf.Max(0, value);
    }
    //public float Mana
    //{
    //    get => mana;
    //    set => mana = Mathf.Max(0, value);
    //}
    public float MaxSpeed
    {
        get => maxSpeed;
        set => maxSpeed = Mathf.Max(1, value);
    }
}
