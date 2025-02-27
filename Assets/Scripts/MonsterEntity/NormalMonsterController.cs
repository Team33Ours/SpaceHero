using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 일반몬스터의 이동,공격 기능
/// 보스몬스터는 다른 Controller를 통해 이동,공격,스킬 적용한다
/// 2025.02.24.ImSeonggyun
/// </summary>
public class NormalMonsterController : BaseController
{
    //private MonsterManager monsterManager;
    public MonsterManager monsterManager;
    //private Transform target;
    public Transform target;

    // 몬스터 종류에 따라 다른 값들
    //[SerializeField] private float followRange;
    [SerializeField] public float followRange;

    public void Initialize(MonsterManager _monsterManager, Transform _target, float _followRange)
    {
        monsterManager = _monsterManager;
        target = _target;

        // 몬스터마다 값이 다르므로 생성시에 지정해준다
        followRange = _followRange;
    }
    // 몬스터(일반)의 이동로직
    protected override void HandleAction()
    {
        // OOP 특강때 지우지 말라고 했던 것이 생각났다
        base.HandleAction();

        if (weaponHandler == null || target == null)
        {
            if (!movementDirection.Equals(Vector2.zero)) movementDirection = Vector2.zero;
            return;
        }


        // 거리,방향 구하기
        float distance = DistanceToTarget();
        Vector2 direction = DirectionToTarget();   

        // 공격하고 있지 않다
        isAttacking = false;

        // 거리에 따른 판단
        if (distance <= followRange)
        {
            // 이동
            lookDirection = direction;
            // 공격범위에 있다면 
            if (distance <= weaponHandler.AttackRange)
            {
                // 공격
                int layerMaskTarget = weaponHandler.target;
                RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, weaponHandler.AttackRange * 1.5f, 
                    (1 << LayerMask.NameToLayer("Level")) | layerMaskTarget);

                // 충돌체가 있다면, 충돌해서 처리해야하는 layer가 맞는지 확인한다
                // layer가 "Level"의 layer이면 공격하지 않는다
                if (hit.collider != null && layerMaskTarget == (layerMaskTarget | (1 << hit.collider.gameObject.layer)))
                {
                    isAttacking = true;
                }
                /// 이건 0으로 바꿀 필요가 있을까?
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

    /// <summary>
    /// 몬스터의 경우 pivot의 방향을 가리킨다
    /// 몬스터의 방향이 바뀌지 않는다
    /// </summary>
    /// <returns></returns>
    protected Vector2 DirectionToTarget()
    {
        return (target.position - transform.position).normalized;
    }
    public override void Death()
    {
        base.Death();
        // 죽은 건 오브젝트 풀링을 적용하여 List에 집어넣는다
        monsterManager.RemoveMonsterOnDeath(gameObject);
    }
}
