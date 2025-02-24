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

    private ProjectileManager projectileManager;
    protected override void Start()
    {
        base.Start();
        projectileManager = ProjectileManager.Instance;
    }
    public override void Attack()
    {
        base.Attack();

        float projectilesAngleSpace = multipleProjectilesAngle;  // ������ ź�� ���� ����
        int numberOfProjectilesPerShot = numberofProjectilesPerShot;    // ����� �� ���ΰ�

        // �߻��ؾ��ϴ� �ּҰ��� 
        float minAngle = -(numberOfProjectilesPerShot / 2f) * projectilesAngleSpace;


        for (int i = 0; i < numberOfProjectilesPerShot; i++)
        {
            float angle = minAngle + projectilesAngleSpace * i;
            float randomSpread = Random.Range(-spread, spread); // ������ ź ������ ����
            angle += randomSpread;  // ��ä�ο� ������ ź�� ������
            CreateProjectile(Controller.LookDirection, angle);
        }
    }

    private void CreateProjectile(Vector2 _lookDirection, float angle)
    {
        // ������ �� �Ŵ����� ���� �����ϰ� �߻�
        projectileManager.ShootBullet(this, projectileSpawnPosition.position, RotateVector2(_lookDirection, angle));
    }
    private static Vector2 RotateVector2(Vector2 v, float degree)
    {
        return Quaternion.Euler(0, 0, degree) * v;  // ȸ����ġ��ŭ ����(v)�� ȸ��
        /// ����: ���ʹϾ� * ������ ��ȯ��Ģ�� �������� �ʴ´�
    }

}
