using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetEXP : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            UIManager.Instance.EXP += 10;
            Debug.Log($"{UIManager.Instance.EXP}");

            if(UIManager.Instance.EXP > 150)
            {
                UIManager.Instance.LevelUP();
            }
            Destroy(gameObject);
        }
    }
}
