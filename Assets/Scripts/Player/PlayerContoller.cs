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

        // Vector2 mousePosition = Input.mousePosition;
        // Vector2 worldPos = camera.ScreenToWorldPoint(mousePosition);
        // lookDirection = (worldPos - (Vector2)transform.position);
    }

    public void Attack(bool isAttack, Transform target)
    {
        isAttacking = isAttack;
        lookDirection = ((Vector2)target.position - (Vector2)transform.position).normalized;
    }
}