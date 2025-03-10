using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    [SerializeField] private SpriteRenderer characterRenderer;
    private static readonly int IsMove = Animator.StringToHash("IsMove");
    private static readonly int IsAttack = Animator.StringToHash("IsAttack");
    private static readonly int IsDamage = Animator.StringToHash("IsDamage");
    private static readonly int IsDie = Animator.StringToHash("IsDie");

    private static readonly int MoveX = Animator.StringToHash("MoveX");
    private static readonly int MoveY = Animator.StringToHash("MoveY");

    protected Animator animator;

    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void Move(Vector2 obj)
    {
        animator.SetBool(IsMove, obj.magnitude > .5f);

        bool isRight = obj.x > 0f;
        characterRenderer.flipX = isRight;

        Debug.LogFormat($"{animator.transform.parent}  + IsMove + {animator.GetBool("IsMove")}");

        animator.SetFloat(MoveX, obj.x);
        animator.SetFloat(MoveY, obj.y);
    }

    public void Attack(bool isAttack)
    {
        Debug.LogFormat($"{animator.transform.parent}  + IsAttack + {animator.GetBool("IsAttack")}");

        animator.SetBool(IsAttack, isAttack);
    }

    public void Damage()
    {
        Debug.LogFormat($"{animator.transform.parent}  + IsDamage + {animator.GetBool("IsDamage")}");

        animator.SetBool(IsDamage, true);
    }
    public void Die()
    {
        animator.SetTrigger(IsDie);
    }

    public void InvincibilityEnd()
    {
        animator.SetBool(IsDamage, false);
        
        
    }
    
}