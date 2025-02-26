using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] private float smooth = 0.1f;
    [SerializeField] private Vector2 maxCameraBounds;
    [SerializeField] private Vector2 minCameraBounds;
        
    private void FixedUpdate()
    {
        if (target == null)
            return;
        
        Vector3 targetPos = new Vector3(target.position.x, target.position.y, this.transform.position.z);

        targetPos.x = Mathf.Clamp(targetPos.x, minCameraBounds.x, maxCameraBounds.x);
        targetPos.y = Mathf.Clamp(targetPos.y, minCameraBounds.y, maxCameraBounds.y);

        transform.position = Vector3.Lerp(transform.position, targetPos, smooth);
    }
}
