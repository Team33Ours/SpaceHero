using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager Instance;
    private Slider hpBar;
    private TextMeshProUGUI currentHpText;

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
        hpBar = GetComponentInChildren<Slider>();
        currentHpText = GetComponentInChildren<TextMeshProUGUI>(true);
    }

    //public void UpdateHP()
    //{
    //    hpDifferenceCheck = TempPlayer.instance.Stat.currentHP;
    //    HPSlider.value = TempPlayer.instance.Stat.currentHP;
    //    HPText.text = $"{HPSlider.value:F0}";
    //}
}
