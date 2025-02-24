using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ٰŸ� ������ Handler
/// 2025.02.24.ImSeonggyun
/// </summary>
public class MeleeWeaponHandler : WeaponHandler
{
    [Header("Melee Attack Info")]
    public Vector2 collideBoxSize = Vector2.one;    // �ٰŸ� ���� �浹 �Ÿ�


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        collideBoxSize = collideBoxSize * WeaponSize;   // collider ������ ����
    }

}
