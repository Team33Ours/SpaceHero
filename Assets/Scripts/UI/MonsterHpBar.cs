using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHpBar : TempMonster
{
    private TempStatus status;

    private void Start()
    {
        status = base.Stat;
    }
}
