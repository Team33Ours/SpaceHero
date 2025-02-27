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
        if(GameManager.Instance.playerController == null)
            GameManager.Instance.playerController = this;
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

    public override void Death()
    {
        lookDirection = Vector2.zero;
        movementDirection = Vector2.zero;
        animationHandler.Die();
        isDead = true;
        
        foreach (SpriteRenderer renderer in transform.GetComponentsInChildren<SpriteRenderer>())
        {
            // a값만 바꾼다
            Color color = renderer.color;
            color.a = 0.3f;
            renderer.color = color;
        }
        
        // 플레이어 사망시 무기 작동 안하게 null처리
        // weaponHandler.SaveItemInfo();
        weaponHandler = null;
        
        
        StartCoroutine(WaitForDead());
    }
    IEnumerator WaitForDead()
    {
        // 사망 애니메이션 시간 2.5초
        yield return new WaitForSeconds(2.5f);
        gameObject.SetActive(false);
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