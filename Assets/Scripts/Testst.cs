using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testst : MonoBehaviour
{
    GameObject player;
    void Start()
    {
        player = GameObject.Find("Player"); // 하이러키에서 'Player'라는 이름을 가진 오브젝트 찾기
        GameManager.Instance.player = this.player; // GameManager의 player 변수에 찾은 player 오브젝트를
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
