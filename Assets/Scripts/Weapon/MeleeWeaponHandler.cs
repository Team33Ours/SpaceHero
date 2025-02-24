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
    public override void Attack()
    {
        base.Attack();

        /// ��ü�� ���� �ִ� raycast
        // origin���� ������ġ���� �ٶ� �������� �̵��� ��ǥ
        // �� �ڸ������� �� ���̹Ƿ� distance�� 0
        RaycastHit2D hit = Physics2D.BoxCast(transform.position + (Vector3)Controller.LookDirection * collideBoxSize.x,
            collideBoxSize, 0, Vector2.zero, 0, target);

        if (hit.collider != null)
        {
            // ü�°����� ResourceController���� 
            ResourceController resourceController = hit.collider.GetComponent<ResourceController>();
            if (resourceController != null)
            {
                resourceController.ChangeHealth(-Power);
                // �˹��� �����ִٸ�
                if (IsOnKnockback)
                {
                    // BaseController�� ����
                    BaseController controller = hit.collider.GetComponent<BaseController>();
                    if (controller != null)
                    {
                        controller.ApplyKnockback(transform, KnockbackPower, KnockbackTime);
                    }
                }
            }
        }
    }
    // �ٰŸ� ������ rotate�� ���Ÿ� ������ rotate�� �ٸ���
    public override void Rotate(bool isLeft)
    {
        // �ٰŸ� ����� y������ ȸ���ؾ� rotate�� �� ����ȴ�
        if (isLeft)
            transform.eulerAngles = new Vector3(0, 180, 0);
        else
            transform.eulerAngles = new Vector3(0, 0, 0);
    }

}
