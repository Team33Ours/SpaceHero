using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// �Ϲݸ����� �̵�,���� ���
/// 2025.02.24.ImSeonggyun
/// </summary>
public class NormalMonsterController : BaseController
{
    private MonsterManager monsterManager;
    private Transform target;

    // ���� ������ ���� �ٸ� ����
    [SerializeField] private float followRange;

    public void Initialize(MonsterManager _monsterManager, Transform _target, float _followRange)
    {
        monsterManager = _monsterManager;
        target = _target;

        // ���͸��� ���� �ٸ��Ƿ� �����ÿ� �������ش�
        followRange = _followRange;
    }
    // ����(�Ϲ�)�� �̵�����
    protected override void HandleAction()
    {
        // OOP Ư���� ������ ����� �ߴ� ���� ��������
        base.HandleAction();

        // ���Ͱ� ���� ������ �߰�

        // �Ÿ�,���� ���ϱ�
        float distance = DistanceToTarget();
        Vector2 direction = DirectionToTarget();

        // �����ϰ� ���� �ʴ�
        isAttacking = false;

        // �Ÿ��� ���� �Ǵ�
        if (distance <= followRange)
        {
            // �̵�
            lookDirection = direction;
            // ���ݹ����� �ִٸ� 
            if (distance <= weaponHandler.AttackRange)
            {
                // ����
                int layerMaskTarget = weaponHandler.target;
                RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, weaponHandler.AttackRange * 1.5f, 
                    (1 << LayerMask.NameToLayer("Level")) | layerMaskTarget);

                // �浹ü�� �ִٸ�, �浹�ؼ� ó���ؾ��ϴ� layer�� �´��� Ȯ���Ѵ�
                // layer�� "Level"�� layer�̸� �������� �ʴ´�
                if (hit.collider != null && layerMaskTarget == (layerMaskTarget | (1 << hit.collider.gameObject.layer)))
                {
                    isAttacking = true;
                }
                movementDirection = Vector2.zero;
                return;
            }
            movementDirection = direction;
        }
    }
    protected float DistanceToTarget()
    {
        return Vector3.Distance(transform.position, target.position);
    }
    protected Vector2 DirectionToTarget()
    {
        return (target.position - transform.position).normalized;
    }
    public override void Death()
    {
        base.Death();
        // ���� �� ������Ʈ Ǯ���� �����Ͽ� List�� ����ִ´�
        monsterManager.RemoveMonsterOnDeath(this);
    }
}
