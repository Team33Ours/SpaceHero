using UnityEngine;
using System.Collections.Generic;

// This is a temporary game object class used in the UI test scene.

public class TempGameObject : MonoBehaviour
{
    public TempStatus Stat;
    public List<TempSkill> hasSkills;

    private void Awake()
    {
        // UI에 스탯 정보 주입
        GameObjectUI uiNeedStat = GetComponent<GameObjectUI>();
        uiNeedStat.ObjectStat = Stat;
    }

    public void GetSkill(TempSkill newSkill)
    {
        hasSkills.Add(newSkill);
    }
}