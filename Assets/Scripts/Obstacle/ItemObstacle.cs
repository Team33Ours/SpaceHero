using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObstacle : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            AchievementManager.Instance.CheckAchievementProgress("10002", 1);
            Destroy(gameObject);
        }
    }
}
