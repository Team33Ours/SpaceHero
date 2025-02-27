using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

/// <summary>
/// 현재는 테스트를 위해 스테이지를 관리하는 클래스
/// 2025.02.25.한만진
/// </summary>
public class GameManager : Singleton<GameManager>
{
    public int killCount = 0;
    public int enemyCount = 0;
    public int stage = 1;
    ObstacleSpawner obstacleSpawner;

    public PlayerController playerController; // 몬스터 생성시 타겟에 넣어야 한다

    public GameObject currentStage;
    //public GameObject player;
    protected override void Awake()
    {
        base.Awake();
        obstacleSpawner = GetComponent<ObstacleSpawner>();

        /// 디버그용으로 하이러키에 올려놓은 Player와 연결한다
        playerController = FindObjectOfType<PlayerController>();    // 실패. 프리팹이 연결된다
        //GameObject playerObj = GameObject.Find("Player"); // 하이러키에서 'Player'라는 이름을 가진 오브젝트 찾기
        //if (playerObj != null)
        //{
        //    playerController = playerObj.GetComponent<PlayerController>();
        //}sd
    }

    void Start()
    {
        currentStage = obstacleSpawner.CreateFloorTiles((stage - 1) / 10, 3, 5, 5);
    }
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    AddKillCount();
        //}
    }
    public void AddKillCount()
    {
        killCount++;
        if (killCount >= enemyCount)
        {
            killCount = 0;
            currentStage.GetComponentInChildren<DoorOpener>().UnlockDoor();
            currentStage.GetComponentInChildren<BoxCollider2D>().enabled = true;
            NextStage(); // test
        }
    }

    public void NextStage()
    {
        stage++;
        //UIManager.Instance.AcendStage();
        //enemyCount = 0;    
        Destroy(currentStage);
        currentStage = obstacleSpawner.CreateFloorTiles((stage - 1) / 10, 3, 5, 5);

        //player.transform.position = new Vector3(0, -5, 0);
    }

    public void AddEnemyCount()
    {
        enemyCount++;
    }
}
