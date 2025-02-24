using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���Ÿ� ������ Handler
/// 2025.02.24.ImSeonggyun
/// </summary>
public class RangeWeaponHandler : WeaponHandler
{
    [Header("Ranged Attack Data")]
    [SerializeField] private Transform projectileSpawnPosition;

    [SerializeField] private int bulletIndex;   // �Ѿ��� �ε����� �����ͼ� � ���� ������� ���Ѵ�
    public int BulletIndex { get { return bulletIndex; } }

    [SerializeField] private float bulletSize = 1;
    public float BulletSize { get { return bulletSize; } }

    [SerializeField] private float duration;    // �Ѿ��� ���� �Ⱓ
    public float Duration { get { return duration; } }

    [SerializeField] private float spread;  // ź ������ ����
    public float Spread { get { return spread; } }

    [SerializeField] private int numberofProjectilesPerShot;    // ����� �� ���ΰ�
    public int NumberofProjectilesPerShot { get { return numberofProjectilesPerShot; } }

    [SerializeField] private float multipleProjectilesAngle;    // ������ ź�� ���� ����
    public float MultipleProjectilesAngel { get { return multipleProjectilesAngle; } }

    [SerializeField] private Color projectileColor;     // �Ѿ��� ������ �پ��ϰ�
    public Color ProjectileColor { get { return projectileColor; } }



}
