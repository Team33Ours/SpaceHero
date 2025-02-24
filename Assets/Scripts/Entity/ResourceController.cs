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
