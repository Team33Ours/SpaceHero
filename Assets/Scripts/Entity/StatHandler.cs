using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatHandler : MonoBehaviour
{
    // ????? ?????? ???? ????????? ??????
    [SerializeField] private float maxHealth;    // ?????? 
    //[SerializeField] private float health;       // ??ð? ???
    [SerializeField] private float maxMana;    // ??븶??
    //[SerializeField] private float mana;       // ??ð? ????
    [SerializeField] private float maxSpeed;       // ?????
    // ?÷????, ???? ??????? ??????
    public float MaxHealth
    {
        get => maxHealth;
        set => maxHealth = Mathf.Max(1, value); // ??? 1 ??????? ????
    }
    //public float Health
    //{
    //    get => health;
    //    set => health = Mathf.Max(0, value);    // ??? 0 ??????? ????
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
