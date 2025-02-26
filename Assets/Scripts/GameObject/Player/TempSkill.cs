using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillRarity
{
    Uqique,
    Rare,
    Common
}

[CreateAssetMenu(fileName = "Skill", menuName = "custum/Skills", order = 1)]
public class TempSkill : ScriptableObject
{
    public SkillRarity skillRarity;
    public string Name;
    public string Description;

    // 스킬 구현
    public void ApplySkill()
    {
        Debug.Log($"Use Skill : {Name}");
    }
}
