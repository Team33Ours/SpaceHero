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

    protected override void Awake()
    {
        base.Awake();
        obstacleSpawner = GetComponent<ObstacleSpawner>();
        
    }

    void Start()
    {
        currentStage = obstacleSpawner.CreateFloorTiles((stage - 1) / 10, 3, 100);
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
            Debug.Log("Stage Clear");
            UIManager.Instance.AcendStage();
            killCount = 0;

            NextStage();
        }
    }

    public void NextStage()
    {
        stage++;
        //enemyCount = 0;    
        Destroy(currentStage);
        currentStage = obstacleSpawner.CreateFloorTiles((stage - 1) / 10, 3, 100);
    }

    public void AddEnemyCount()
    {
        enemyCount++;
    }


}
