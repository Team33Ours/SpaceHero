using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 원거리 무기의 Handler
/// 2025.02.24.ImSeonggyun
/// </summary>
public class RangeWeaponHandler : WeaponHandler
{
    [Header("Ranged Attack Data")]
    [SerializeField] private Transform projectileSpawnPosition;

    [SerializeField] private int bulletIndex;   // 총알의 인덱스를 가져와서 어떤 총을 사용할지 정한다
    public int BulletIndex { get { return bulletIndex; } }

    [SerializeField] private float bulletSize = 1;
    public float BulletSize { get { return bulletSize; } }

    [SerializeField] private float duration;    // 총알의 생존 기간
    public float Duration { get { return duration; } }

    [SerializeField] private float spread;  // 탄 퍼짐의 정도
    public float Spread { get { return spread; } }

    [SerializeField] private int numberofProjectilesPerShot;    // 몇발을 쏠 것인가
    public int NumberofProjectilesPerShot { get { return numberofProjectilesPerShot; } }

    [SerializeField] private float multipleProjectilesAngle;    // 각각의 탄의 퍼짐 정도
    public float MultipleProjectilesAngel { get { return multipleProjectilesAngle; } }

    [SerializeField] private Color projectileColor;     // 총알의 색깔을 다양하게
    public Color ProjectileColor { get { return projectileColor; } }



}
