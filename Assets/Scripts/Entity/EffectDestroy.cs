using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDestroy : MonoBehaviour
{
    public float Time = 0.5f;
    void Start()
    {
        //Time 후 Object 삭제
        Destroy(gameObject, Time);
    }
}
