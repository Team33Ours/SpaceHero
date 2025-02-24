using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempPlayer : MonoBehaviour
{
    public TempPlayerStatus Stat;

    public float ReturnMaxHP()
    {
        return Stat.maxHP;
    }

    public float ReturnCurrentHP()
    {
        return Stat.currentHP;
    }
}
