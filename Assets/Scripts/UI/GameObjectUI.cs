using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GameObjectUI : MonoBehaviour
{
    // Status should be injected from the game object class.
    internal TempStatus ObjectStat = null;
    private bool isHealthPointDifferent;
    private float hpDifferenceCheck;

    private Slider HPSlider;
    private TextMeshProUGUI HPText;


    private void Awake()
    {
        HPSlider = GetComponentInChildren<Slider>();
        HPText = GetComponentInChildren<TextMeshProUGUI>(true);
    }

    private void Start()
    {
        ReadObjectStatus();
    }
    private void ReadObjectStatus()
    {
        HPSlider.maxValue = ObjectStat.maxHP;
        HPSlider.value = ObjectStat.currentHP;
        hpDifferenceCheck = ObjectStat.currentHP;
        HPText.text = $"{HPSlider.value:F0}";
    }
    
    private void Update()
    {
        if (hpDifferenceCheck != ObjectStat.currentHP)
            UpdateHP();
    }

    private void UpdateHP()
    {
        hpDifferenceCheck = ObjectStat.currentHP;
        HPSlider.value = ObjectStat.currentHP;
        HPText.text = $"{HPSlider.value:F0}";
    }
}
