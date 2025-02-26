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

    public GameObject currentStage;
    public GameObject player;
    protected override void Awake()
    {
        base.Awake();
        obstacleSpawner = GetComponent<ObstacleSpawner>();
        
    }

    void Start()
    {
        currentStage = obstacleSpawner.CreateFloorTiles((stage - 1) / 10, 3, 0);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AddKillCount();
        }
    }
    public void AddKillCount()
    {
        killCount++;
        if (killCount >= enemyCount)
        {
            killCount = 0;
            currentStage.GetComponentInChildren<DoorOpener>().UnlockDoor();
            currentStage.GetComponentInChildren<BoxCollider2D>().enabled = true;
        }
    }

    public void NextStage()
    {
        stage++;
        UIManager.Instance.AcendStage();
        //enemyCount = 0;    
        Destroy(currentStage);
        currentStage = obstacleSpawner.CreateFloorTiles((stage - 1) / 10, 3, 100);

        player.transform.position = new Vector3(0, -5, 0);
    }

    public void AddEnemyCount()
    {
        enemyCount++;
    }
}
