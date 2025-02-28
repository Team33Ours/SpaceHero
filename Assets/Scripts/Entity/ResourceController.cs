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
    private AnimationHandler animationHandler;

    public Status Status;
    private Status instanceStat;
    internal float currentHP;

    private float timeSinceLastChange = float.MaxValue; // 변화를 가진 시간 저장하여, 일정시간후에 다시 변화를 받는다

    //public float MaxMana => statHandler.MaxMana;    // 캐릭터, 몬스터의 기본 마나
    //public float CurrentMana { get; private set; }

    public List<PlayerSkill> hasSkills;

    public AudioClip damageClip;   // 피격 사운드 

    private Action<float, float> OnChangeHealth;    // delegate를 통한 이벤트 호출
    private Action<float, float> OnChangeMana;    // delegate를 통한 이벤트 호출
    private Action<float, float> OnChangeSpeed;    // delegate를 통한 이벤트 호출

    private void Awake()
    {
        animationHandler = GetComponent<AnimationHandler>();
        baseController = GetComponent<BaseController>();
        instanceStat = Instantiate(Status);
        currentHP = Status.maxHealth;

        // UI에 스탯 정보 주입
        GameObjectUI uiNeedStat = GetComponent<GameObjectUI>();
        uiNeedStat.ObjectStat = Status;

        // BaseController에 정보 주입
        baseController.Status = Status;

        // UpStatusFromSkill에 정보 주입
        if (this.gameObject == CompareTag("Player"))
        {
            UpStatusFromSkill upStatus = GetComponent<UpStatusFromSkill>();
            upStatus.playerStatus = Status;
        }
    }

    private void OnEnable()
    {
        currentHP = Status.maxHealth;
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
        currentHP += change;    // +: 회복, -: 데미지
        currentHP = currentHP > Status.maxHealth ? Status.maxHealth : currentHP;  // 체력 Max
        currentHP = currentHP < 0 ? 0 : currentHP;  // 체력 Min

        // HP 변화량이 생겼을 때 호출
        /// OnChangeHealth delegate에 연결된 메서드가 있다면, CurrentHealth와 MaxHealth 메서드를 매개변수로 전달해서 호출한다
        OnChangeHealth?.Invoke(currentHP, Status.maxHealth);

        if (change < 0)
        {
            animationHandler.Damage();

            if (damageClip != null)
                SoundManager.PlayClip(damageClip);
        }

        if (currentHP <= 0)
        {
            Death();
            if (this.gameObject != CompareTag("Player"))
            {
                // 보석 프리팹 동적 생성 1~2개
            }
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
    //#region Mana
    //public bool ChangeMana(float change)
    //{
    //    // mana는 딜레이같은거 없다
    //    if (change == 0)
    //        return false;

    //    CurrentMana += change;
    //    CurrentMana = CurrentMana > MaxMana ? MaxMana : CurrentMana;
    //    CurrentMana = CurrentMana < 0 ? 0 : CurrentMana;
    //    return true;
    //}
    //public void AddManaChangeEvent(Action<float, float> action)
    //{
    //    OnChangeMana += action;
    //}
    //public void RemoveManaChangeEvent(Action<float, float> action)
    //{
    //    OnChangeMana -= action;
    //}
    //#endregion
    #region Speed
    public bool ChangeSpeed(float change)
    {
        if (change == 0)
            return false;
        Status.currentSpeed += change;

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

    // 몬스터의 스킬에 의한 체력감소,스피드감소 효과
    public void TakeDamage(float damage)
    {
        currentHP = currentHP > damage ? (currentHP - damage) : 0;
    }
    public IEnumerator TakeDamageAndDebuff(float damage, float speed, float time)
    {
        currentHP -= damage;
        float delta;
        if (Status.currentSpeed > speed)
        {
            delta = speed;
        }
        else
        {
            delta = Status.currentSpeed;
        }
        Status.currentSpeed -= delta;
        yield return new WaitForSeconds(time);  // 지속시간
        Status.currentSpeed += delta;      // 원래대로
    }
}
