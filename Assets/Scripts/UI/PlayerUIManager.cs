using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PlayerUIManager : MonoBehaviour
{
    public static PlayerUIManager Instance;
    private TempPlayer tempPlayerInstance;
    private bool isHealthPointDifferent;
    private float hpDifferenceCheck;

    private Slider HPSlider;
    private TextMeshProUGUI HPText;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        HPSlider = GetComponentInChildren<Slider>();
        HPText = GetComponentInChildren<TextMeshProUGUI>(true);
    }

    private void Start()
    {
        // Player 정보 읽음
        tempPlayerInstance = TempPlayer.instance;
        if (TempPlayer.instance != null)
            ReadPlayerInstance();


    }
    private void ReadPlayerInstance()
    {
        HPSlider.maxValue = TempPlayer.instance.Stat.maxHP;
        HPSlider.value = TempPlayer.instance.Stat.currentHP;
        hpDifferenceCheck = HPSlider.value;
        HPText.text = HPSlider.value.ToString();
    }

    private void Update()
    {
        if (hpDifferenceCheck != TempPlayer.instance.Stat.currentHP)
            UpdateHP();
    }

    private void UpdateHP()
    {
        hpDifferenceCheck = TempPlayer.instance.Stat.currentHP;
        HPSlider.value = TempPlayer.instance.Stat.currentHP;
        HPText.text = $"{HPSlider.value:F0}";
    }
}
