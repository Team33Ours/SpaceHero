using UnityEngine;

public class TempPlayer : MonoBehaviour
{
    public TempStatus Stat;

    private void Awake()
    {
        GameObjectUI uiNeedStat = GetComponent<GameObjectUI>();
        uiNeedStat.ObjectStat = Stat;
    }

    private void Update()
    {
        // Å×½ºÆ®
        Stat.currentHP -= Time.deltaTime;
    }
}
