using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// 
/// </summary>
public class RangeWeaponHandler : WeaponHandler
{

    [Header("Ranged Attack Data")]
    [SerializeField] private Transform projectileSpawnPosition;
    
    [SerializeField] private int bulletIndex;
    public int BulletIndex {get{return bulletIndex;}}
    
    [SerializeField] private float bulletSize = 1;
    public float BulletSize {get{return bulletSize;}}
    
    [SerializeField] private float duration;
    public float Duration {get{return duration;}}
    
    [SerializeField] private float spread;
    public float Spread {get{return spread;}}
    
    [SerializeField] private int numberofProjectilesPerShot;
    public int NumberofProjectilesPerShot {get{return numberofProjectilesPerShot;}}
    
    [SerializeField] private float multipleProjectilesAngel;
    public float MultipleProjectilesAngel {get{return multipleProjectilesAngel;}}
    
    [SerializeField] private Color projectileColor;
    public Color ProjectileColor {get{return projectileColor;}}

    private ProjectileManager projectileManager;

    protected override void Start()
    {
        base.Start();
        projectileManager = ProjectileManager.Instance;
        SetWeaponSpeed(Delay);
    }

    public override void Attack()
    {
        base.Attack();
        
        float projectilesAngleSpace = multipleProjectilesAngel;
        int numberOfProjectilesPerShot = numberofProjectilesPerShot;

        float minAngle = -(numberOfProjectilesPerShot / 2f) * projectilesAngleSpace + 0.5f * multipleProjectilesAngel;


        for (int i = 0; i < numberOfProjectilesPerShot; i++)
        {
            float angle = minAngle + projectilesAngleSpace * i;
            float randomSpread = Random.Range(-spread, spread);
            angle += randomSpread;
            CreateProjectile(Controller.LookDirection, angle);
        }
        
    }

    private void CreateProjectile(Vector2 _lookDirection, float angle)
    {
        projectileManager.ShootBullet(
            this,
            projectileSpawnPosition.position,
            RotateVector2(_lookDirection, angle));
    }

    private static Vector2 RotateVector2(Vector2 v, float degree)
    {
        return Quaternion.Euler(0, 0, degree) * v;
    }
    
    
    public override void UpgradeBulletSize(float upgrade)
    {
        // 투사체 크기
        bulletSize += upgrade;
    }
    public override void UpgradeBulletNumber(int upgrade)
    {
        // 투사체 갯수
        numberofProjectilesPerShot += upgrade;
        // 투사체 발사 각도 ++점점 넓어짐
        multipleProjectilesAngel += upgrade * 2;
    }

    // 보스몬스터 총알교체
    public void ChangeBossBullet(Vector2 _lookDirection, float angle)
    {
        projectileManager.ChangeBossBullet(this, projectileSpawnPosition.position,
            RotateVector2(_lookDirection, angle), 4);
    }
}
