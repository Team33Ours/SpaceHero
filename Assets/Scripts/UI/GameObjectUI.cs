using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GameObjectUI : MonoBehaviour
{
    // 스테이터스는 오브젝트로부터 주입받아야함
    internal Status ObjectStat = null;
    private float hpDifferenceCheck;

    // 현재 HP 참조용
    ResourceController getCurrentHp;

    private Slider HPSlider;
    private TextMeshProUGUI HPText;


    private void Awake()
    {
        HPSlider = GetComponentInChildren<Slider>();
        HPText = GetComponentInChildren<TextMeshProUGUI>(true);
        getCurrentHp = GetComponent<ResourceController>();
    }

    private void Start()
    {
        if (ObjectStat != null)
            ReadObjectStatus();
    }

    private void ReadObjectStatus()
    {
        HPSlider.maxValue = ObjectStat.maxHealth;
        HPSlider.value = getCurrentHp.currentHP;
        hpDifferenceCheck = getCurrentHp.currentHP;
        HPText.text = $"{HPSlider.value:F0}";
    }
    
    private void Update()
    {
        if ((hpDifferenceCheck != getCurrentHp.currentHP) && ObjectStat != null)
            UpdateHP();
    }

    private void UpdateHP()
    {
        hpDifferenceCheck = getCurrentHp.currentHP;
        HPSlider.value = getCurrentHp.currentHP;
        HPText.text = $"{HPSlider.value:F0}";
    }
}
