using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpStatusFromSkill : MonoBehaviour
{
    /* 스테이터스 - Status
    * 현재체력 & 가지고 있는 스킬 - ResourceController
    * 무기 대미지, 공격 속도 함수 - PlayerController의 핸들러를 가진 BaseController
    * 투사체 관련 함수 - RengeWeaponHandler를 WeaponHandler로 넣자
    */


    internal Status playerStatus;
    private ResourceController resource;
    private BaseController baseController;

    private void Awake()
    {
        resource = GetComponent<ResourceController>();
        baseController = GetComponent<BaseController>();
    }

    public void GetSkill(PlayerSkill newSkill)
    {
        resource.hasSkills.Add(newSkill);
        string skillType = newSkill.skillType.ToString();
        switch (skillType)
        {
            case "Attack":
                baseController.weaponHandler.UpgradeDamage(newSkill.value * 0.1f);
                break;
            case "AttackSpeed":
                baseController.weaponHandler.UpgradeDelay(newSkill.value * 0.01f);
                break;
            case "Denfence":
                playerStatus.defence += (int)(newSkill.value *0.1f);
                break;
            case "Health":
                playerStatus.maxHealth += newSkill.value;
                resource.currentHP = (resource.currentHP + newSkill.value > playerStatus.maxHealth) ? playerStatus.maxHealth : resource.currentHP + newSkill.value;
                break;
            case "MoveSpeed":
                playerStatus.currentSpeed += newSkill.value * 0.1f;
                break;
            case "ProjectileAmount":
                GameManager.Instance.playerController.weaponHandler.UpgradeBulletNumber(newSkill.value);
                break;
            case "projectileSize":
                GameManager.Instance.playerController.weaponHandler.UpgradeBulletSize(newSkill.value * 0.1f);
                break;
        }
    }
}
