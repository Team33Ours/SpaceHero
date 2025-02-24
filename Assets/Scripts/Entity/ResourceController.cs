using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �÷��̾�� ������ �ǽð� �ڿ�(ü��,����)�� ��ȭ
/// 2025.02.24.ImSeonggyun
/// </summary>
public class ResourceController : MonoBehaviour
{
    [SerializeField] private float healthChangeDelay = .5f; // �����⵿ֱ�� ��������

    private BaseController baseController;
    private StatHandler statHandler;
    private AnimationHandler animationHandler;

    private float timeSinceLastChange = float.MaxValue; // ��ȭ�� ���� �ð� �����Ͽ�, �����ð��Ŀ� �ٽ� ��ȭ�� �޴´�

    public float MaxHealth => statHandler.MaxHealth;    // ĳ����, ������ �⺻ ü��
    public float CurrentHealth { get; private set; }    // �������踦 ���̱� ���� �ǽð� ü���� ResourceController���� ����,����

    public float MaxMana => statHandler.MaxMana;    // ĳ����, ������ �⺻ ����
    public float CurrentMana { get; private set; }

    public float MaxSpeed => statHandler.MaxSpeed;  // ĳ����, ������ �⺻ ���ǵ�
    public float CurrentSpeed { get; private set; }    // �ӵ� ��� ���� �Ǵ� �������� ������ �Ͻ������� �������� �߰��ӵ�

    public AudioClip damageClip;   // �ǰ� ���� 

    private Action<float, float> OnChangeHealth;    // delegate�� ���� �̺�Ʈ ȣ��
    private Action<float, float> OnChangeMana;    // delegate�� ���� �̺�Ʈ ȣ��
    private Action<float, float> OnChangeSpeed;    // delegate�� ���� �̺�Ʈ ȣ��

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
    public bool ChangeHealth(float change)
    {
        // invincible
        if (change == 0 || timeSinceLastChange < healthChangeDelay)
        {
            return false;
        }

        timeSinceLastChange = 0f;   // ������ �޾����� �ð��� 0���� �ٲپ� ��� ��������
        CurrentHealth += change;    // +: ȸ��, -: ������
        CurrentHealth = CurrentHealth > MaxHealth ? MaxHealth : CurrentHealth;  // ü�� Max
        CurrentHealth = CurrentHealth < 0 ? 0 : CurrentHealth;  // ü�� Min

        // HP ��ȭ���� ������ �� ȣ��
        /// OnChangeHealth delegate�� ����� �޼��尡 �ִٸ�, CurrentHealth�� MaxHealth �޼��带 �Ű������� �����ؼ� ȣ���Ѵ�
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
    public void AddHealthChangeEven(Action<float, float> action)
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
        // mana�� �����̰����� ����
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

        // ������,������ ���� �ӵ������ �Ͻ����̹Ƿ� Ư�� �ð��� ������ �ӵ��� �ٽ� ���������Ѵ�
        // ���� �޼��带 ȣ���ϵ簡, ���⿡ �ٽ� �ۼ��Ѵ�
        // �����ð��ڿ� �ڵ�ȣ��Ǿ� �ӵ��� �ٽ� ������ �ؾ��Ѵ�

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
