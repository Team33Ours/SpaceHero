using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어와 몬스터의 실시간 자원(체력,마력)의 변화
/// 2025.02.24.ImSeonggyun
/// </summary>
public class ResourceController : MonoBehaviour
{
    [SerializeField] private float healthChangeDelay = .5f; // 일정주기동안 무적상태

    private BaseController baseController;
    private StatHandler statHandler;
    private AnimationHandler animationHandler;

    private float timeSinceLastChange = float.MaxValue; // 변화를 가진 시간 저장하여, 일정시간후에 다시 변화를 받는다

    public float MaxHealth => statHandler.MaxHealth;    // 캐릭터, 몬스터의 기본 체력
    public float CurrentHealth { get; private set; }    // 의존관계를 줄이기 위해 실시간 체력은 ResourceController에서 선언,관리

    public float MaxMana => statHandler.MaxMana;    // 캐릭터, 몬스터의 기본 마나
    public float CurrentMana { get; private set; }

    public float MaxSpeed => statHandler.MaxSpeed;  // 캐릭터, 몬스터의 기본 스피드
    public float CurrentSpeed { get; private set; }    // 속도 향상 마법 또는 아이템을 먹으면 일시적으로 빨라지는 추가속도

    public AudioClip damageClip;   // 피격 사운드 

    private Action<float, float> OnChangeHealth;    // delegate를 통한 이벤트 호출

    private void Awake()
    {
        statHandler = GetComponent<StatHandler>();
        animationHandler = GetComponent<AnimationHandler>();
        baseController = GetComponent<BaseController>();
    }
    private void Start()
    {
        CurrentHealth = MaxHealth;
        CurrentMana = MaxMana;
        CurrentSpeed = MaxSpeed;
    }
    private void Update()
    {        

    }


}
