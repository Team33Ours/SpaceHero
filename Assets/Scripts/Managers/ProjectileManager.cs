using UnityEngine;
using static UnityEngine.UI.Image;

/// <summary>
/// 
/// </summary>
public class ProjectileManager : MonoBehaviour
{
    private static ProjectileManager instance;
    public static ProjectileManager Instance { get { return instance; } }

    [SerializeField] private GameObject[] projectilePrefabs;

    private void Awake()
    {
        instance = this;
    }

    public void ShootBullet(RangeWeaponHandler rangeWeaponHandler, Vector2 startPostiion, Vector2 direction)
    {
        /// 실제 원거리 무기가 생성되어야 할 장소
        GameObject origin = projectilePrefabs[rangeWeaponHandler.BulletIndex];
        GameObject obj = Instantiate(origin, startPostiion, Quaternion.identity);

        ProjectileController projectileController = obj.GetComponent<ProjectileController>();
        projectileController.Init(direction, rangeWeaponHandler);
    }

    // 보스몬스터 총알교체
    public void ChangeBossBullet(RangeWeaponHandler rangeWeaponHandler, Vector2 startPostiion, Vector2 direction, int newIdx)
    {
        if (newIdx >= 0 && newIdx < projectilePrefabs.Length)
        {
            GameObject newObj = projectilePrefabs[newIdx];
            GameObject obj = Instantiate(newObj, startPostiion, Quaternion.identity);

            ProjectileController projectileController = obj.GetComponent<ProjectileController>();
            projectileController.Init(direction, rangeWeaponHandler);
        }
    }
}