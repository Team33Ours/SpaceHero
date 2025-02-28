using UnityEngine;
using System.Collections.Generic;

// This is a temporary game object class used in the UI test scene.

public class TempGameObject : MonoBehaviour
{
    public Status Stat;
    public List<PlayerSkill> hasSkills;

    private void Awake()
    {
        // UI에 스탯 정보 주입
        GameObjectUI uiNeedStat = GetComponent<GameObjectUI>();
        uiNeedStat.ObjectStat = Stat;
    }

    public void GetSkill(PlayerSkill newSkill)
    {
        hasSkills.Add(newSkill);
    }
}