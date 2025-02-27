using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseController
{
    private Camera camera;

    protected override void Start()
    {
        base.Start();
        camera = Camera.main;
    }

    protected override void HandleAction()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        movementDirection = new Vector2(horizontal, vertical).normalized;
        
    }

    public void IsAttack(bool isAttack, Transform target)
    {
        isAttacking = isAttack;
        if (target?.position != null)
        {
            lookDirection = ((Vector2)target?.position - (Vector2)transform.position).normalized;
        }
    }

    //UI에 연결하여 player의 weapon에 업그레이드
    
    public void WeaponUpgradeDamage(float damage)
    {
        weaponHandler.UpgradeDamage(damage);
    }
    public void WeaponUpgradeDelay(float delay)
    {
        weaponHandler.UpgradeDelay(delay);
    }

    public void WeaponUpgradeSpeed(float speed)
    {
        weaponHandler.UpgradeBulletSpeed(speed);
    }

    public void WeaponUpgradeSize(float size)
    {
        weaponHandler.UpgradeBulletSize(size);
    }

    public void WeaponUpgradeNum(int num)
    {
        weaponHandler.UpgradeBulletNumber(num);
    }
}