using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetEXP : MonoBehaviour
{
    internal Status playerStat;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerStat.EXP += 10;
            Destroy(gameObject);
        }
    }
}
