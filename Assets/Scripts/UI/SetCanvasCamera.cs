using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SetCanvasCamera : MonoBehaviour
{
    void Start()
    {
        Camera cam = GetComponent<Camera>();
        cam = Camera.main;
        Canvas canvas = GetComponent<Canvas>();
    }
}
