using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchEnemy : MonoBehaviour
{
   [SerializeField]private PlayerController player;
   private Collider2D playerCollider;
   
   public bool isAttacking = false;
   private Transform target;
   public Collider2D[] colliders;
   public float radius; //범위
   public LayerMask ObjectLayer; // 레이어 선택

   private void Awake()
   {
      playerCollider = GetComponent<Collider2D>();
      radius = playerCollider.bounds.extents.x;
   }

   private void Update()
   {
      colliders = Physics2D.OverlapCircleAll(transform.position, radius, ObjectLayer);
      FindTarget();
   }
   
   void OnDrawGizmos() // 범위 그리기
   {
      Gizmos.color = Color.red;
      Gizmos.DrawWireSphere(transform.position, radius);
   }

   private void FindTarget()
   {
      if (colliders.Length == 0)
      {
         isAttacking = false;
         player.Attack(false, target);
      }
      else
      {
         isAttacking = true;
         target = colliders[0].transform;
         float shortDis = Vector2.Distance(transform.position, target.position);
         foreach (var collider in colliders)
         {
            float dis = Vector2.Distance(transform.position, collider.transform.position);
            if (dis < shortDis)
            {
               target = collider.transform;
               shortDis = dis;
            }
         }
         
         player.Attack(true, target);
      }
   }

}
