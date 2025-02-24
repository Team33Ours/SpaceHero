using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 근거리 무기의 Handler
/// 2025.02.24.ImSeonggyun
/// </summary>
public class MeleeWeaponHandler : WeaponHandler
{
    [Header("Melee Attack Info")]
    public Vector2 collideBoxSize = Vector2.one;    // 근거리 무기 충돌 거리


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        collideBoxSize = collideBoxSize * WeaponSize;   // collider 사이즈 조정
    }

}
