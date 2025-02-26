using UnityEngine;
using System.Collections.Generic;

// This is a temporary game object class used in the UI test scene.

public class TempGameObject : MonoBehaviour
{
    public TempStatus Stat;
    public List<TempSkill> Skill;

    private void Awake()
    {
        GameObjectUI uiNeedStat = GetComponent<GameObjectUI>();
        uiNeedStat.ObjectStat = Stat;
    }

    private void Update()
    {
        // Test
        //Stat.currentHP -= Time.deltaTime * 3;
    }
}