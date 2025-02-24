using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �÷��̾�� ������ ü��,����,�ӵ�
/// 2025.02.24.ImSeonggyun
/// </summary>
public class StatHandler : MonoBehaviour
{
    // ü���� ����ϰ� ���� Ŭ�������� �Ҵ��Ѵ�
    [SerializeField] private float maxHealth;    // �ִ�ü�� 
    [SerializeField] private float health;       // �ǽð� ü��
    [SerializeField] private float maxMana;    // �ִ븶��
    [SerializeField] private float mana;       // �ǽð� ����
    [SerializeField] private float speed;       // ���ǵ�
    // �÷��̾�, ���� �����Ҷ� �Ҵ��Ѵ�
    public float MaxHealth
    {
        get => maxHealth;
        set => maxHealth = Mathf.Max(1, value); // �ּ� 1 �̻����� ����
    }
    public float Health
    {
        get => health;
        set => health = Mathf.Max(0, value);    // �ּ� 0 �̻����� ����
    }
    public float MaxMana
    {
        get => maxMana;
        set => maxMana = Mathf.Max(0, value);
    }
    public float Mana
    {
        get => mana;
        set => mana = Mathf.Max(0, value);
    }
    public float Speed
    {
        get => speed;
        set => speed = Mathf.Max(1, value);
    }
}
