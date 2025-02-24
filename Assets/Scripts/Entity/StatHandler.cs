using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatHandler : MonoBehaviour
{
    // ü���� ����ϰ� ���� Ŭ�������� �Ҵ��Ѵ�
    [SerializeField] private float maxHealth;    // �ִ�ü�� 
    //[SerializeField] private float health;       // �ǽð� ü��
    [SerializeField] private float maxMana;    // �ִ븶��
    //[SerializeField] private float mana;       // �ǽð� ����
    [SerializeField] private float maxSpeed;       // ���ǵ�
    // �÷��̾�, ���� �����Ҷ� �Ҵ��Ѵ�
    public float MaxHealth
    {
        get => maxHealth;
        set => maxHealth = Mathf.Max(1, value); // �ּ� 1 �̻����� ����
    }
    //public float Health
    //{
    //    get => health;
    //    set => health = Mathf.Max(0, value);    // �ּ� 0 �̻����� ����
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
