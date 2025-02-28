using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHpBar : TempMonster
{
    private Status status;

    private void Start()
    {
        status = base.Stat;
    }
}
