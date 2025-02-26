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
    private Action<float, float> OnChangeMana;    // delegate를 통한 이벤트 호출
    private Action<float, float> OnChangeSpeed;    // delegate를 통한 이벤트 호출

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
        if (timeSinceLastChange < healthChangeDelay)
        {
            timeSinceLastChange += Time.deltaTime;
            if (timeSinceLastChange >= healthChangeDelay)
            {
                animationHandler.InvincibilityEnd();
            }
        }
    }
    #region Health
    public virtual bool ChangeHealth(float change)
    {
        // invincible
        if (change == 0 || timeSinceLastChange < healthChangeDelay)
        {
            return false;
        }

        timeSinceLastChange = 0f;   // 데미지 받았으면 시간을 0으로 바꾸어 잠시 무적상태
        CurrentHealth += change;    // +: 회복, -: 데미지
        CurrentHealth = CurrentHealth > MaxHealth ? MaxHealth : CurrentHealth;  // 체력 Max
        CurrentHealth = CurrentHealth < 0 ? 0 : CurrentHealth;  // 체력 Min

        // HP 변화량이 생겼을 때 호출
        /// OnChangeHealth delegate에 연결된 메서드가 있다면, CurrentHealth와 MaxHealth 메서드를 매개변수로 전달해서 호출한다
        OnChangeHealth?.Invoke(CurrentHealth, MaxHealth);

        if (change < 0)
        {
            animationHandler.Damage();

            //if (damageClip != null)
            //    SoundManager.PlayClip(damageClip);

        }
        if (CurrentHealth <= 0)
        {
            Death();
        }
        return true;
    }
    private void Death()
    {
        // BaseController knows who die
        baseController.Death();
    }
    public void AddHealthChangeEvent(Action<float, float> action)
    {
        OnChangeHealth += action;
    }
    public void RemoveHealthChangeEvent(Action<float, float> action)
    {
        OnChangeHealth -= action;
    }
    #endregion
    #region Mana
    public bool ChangeMana(float change)
    {
        // mana는 딜레이같은거 없다
        if (change == 0)
            return false;

        CurrentMana += change;
        CurrentMana = CurrentMana > MaxMana ? MaxMana : CurrentMana;
        CurrentMana = CurrentMana < 0 ? 0 : CurrentMana;
        return true;
    }
    public void AddManaChangeEvent(Action<float, float> action)
    {
        OnChangeMana += action;
    }
    public void RemoveManaChangeEvent(Action<float, float> action)
    {
        OnChangeMana -= action;
    }
    #endregion
    #region Speed
    public bool ChangeSpeed(float change)
    {
        if (change == 0)
            return false;
        CurrentSpeed += change;

        // 아이템,마법에 의한 속도향상은 일시적이므로 특정 시간이 지나면 속도가 다시 느려져야한다
        // 따로 메서드를 호출하든가, 여기에 다시 작성한다
        // 일정시간뒤에 자동호출되어 속도를 다시 내리게 해야한다

        return true;
    }
    public void AddSpeedChangeEvent(Action<float, float> action)
    {
        OnChangeMana += action;
    }
    public void RemoveSpeedChangeEvent(Action<float, float> action)
    {
        OnChangeMana -= action;
    }
    #endregion
}
