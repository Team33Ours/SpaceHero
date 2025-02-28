using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillRarity
{
    Uqique,
    Rare,
    Common
}

public enum SkillType
{
    Attack,
    AttackSpeed,
    Denfence,
    Health,
    MoveSpeed,
    ProjectileAmount,
    projectileSize
}

[CreateAssetMenu(fileName = "Skill", menuName = "custum/Skills", order = 1)]
public class PlayerSkill : ScriptableObject
{
    public SkillRarity skillRarity;
    public SkillType skillType;
    public string Name;
    public string Description;
    public Sprite Icon;
    public GameObject particles;
    public int value;
}
