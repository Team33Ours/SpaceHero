using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.SceneManagement;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;


/// <summary>
/// 생성할 오브젝트를 그룹으로 나타내는 클래스
/// 2025.02.24.한만진
/// </summary>
[System.Serializable]
public class TileGroup
{
    public GameObject floorTile; // 바닥 타일
    public List<GameObject> wallTiles; // 벽 타일
    public List<GameObject> itemObstacle; // 아이템 장애물
}


/// <summary>
/// 오브젝트들의 생성과 배치를 담당하는 클래스
/// 중복된 벽이나 아이템 장애물이 생성되지 않도록 Bounds를 이용하여 위치를 검사
/// 2025.02.24.한만진
/// </summary>
public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField]
    private List<TileGroup> tileGroups;

    private Dictionary<GameObject, List<GameObject>> tileToWalls = new Dictionary<GameObject, List<GameObject>>();
    private Dictionary<GameObject, List<GameObject>> tileToItemObstacle = new Dictionary<GameObject, List<GameObject>>();

    private List<Bounds> wallBoundsList = new List<Bounds>(); // 벽의 Bounds 리스트
    private List<Bounds> itemObstacleBoundsList = new List<Bounds>(); // 아이템 장애물의 Bounds 리스트
    private List<Bounds> enemyBoundsList = new List<Bounds>(); // 적의 Bounds 리스트


    private void Awake()
    {
        InitTileDictionary();
    }


    void Start()
    {
        GameManager.Instance.currentStage = CreateFloorTiles((GameManager.Instance.stage - 1) / 10, 3, 5, 5);
    }
    void InitTileDictionary()
    {
        foreach (var group in tileGroups)
        {
            if (group.floorTile == null || group.wallTiles == null || group.wallTiles.Count == 0)
            {
                Debug.LogWarning("바닥 타일 또는 벽 타일 리스트가 비어 있음!");
                continue;
            }

            // 바닥 타일을 키로, 벽 타일과 아이템 장애물 목록을 값으로 저장
            tileToWalls[group.floorTile] = new List<GameObject>(group.wallTiles);
            tileToItemObstacle[group.floorTile] = new List<GameObject>(group.itemObstacle);
        }
    }

    // 주어진 stage에 맞는 바닥 타일을 생성하고, 해당 타일에 벽 타일과 을 배치
    public GameObject CreateFloorTiles(int stage, int wallTileCount, int ItemObstacleCount, int enemyCount)
    {
        // 특정 바닥 타일을 선택해서 생성. ElementAt: 인덱스에 해당하는 요소를 반환
        GameObject selectedFloorTile = tileToWalls.Keys.ElementAt(stage); // 선택된 바닥 타일을 가져옴 (stage에 맞춰 타일을 선택)
        List<GameObject> wallTiles = tileToWalls[selectedFloorTile]; // 해당 바닥 타일에 맞는 벽 타일 목록
        List<GameObject> itemObstacles = tileToItemObstacle[selectedFloorTile]; // 해당 바닥 타일에 맞는 아이템 장애물 목록

        // 선택된 바닥 타일을 생성
        GameObject instantiatedFloorTile = Instantiate(selectedFloorTile, transform.position, Quaternion.identity);
        wallBoundsList.Clear(); // 벽 Bounds 리스트 초기화
        itemObstacleBoundsList.Clear(); // 아이템 장애물 Bounds 리스트 초기화
        enemyBoundsList.Clear(); // 적 Bounds 리스트 초기화


        // 벽 타일을 랜덤하게 생성
        for (int i = 0; i < wallTileCount; i++)
        {
            GameObject selectedWallTile = wallTiles[Random.Range(0, wallTiles.Count)]; // 벽 타일을 랜덤하게 선택
            WallObstacle wallObstacle = selectedWallTile.GetComponent<WallObstacle>(); // 벽 타일의 WallObstacle 컴포넌트 가져오기
            float wallXPos = Random.Range(wallObstacle.lowPosX, wallObstacle.highPosX);
            int wallYPos = i * 8;

            // 벽 타일을 생성하고 부모 오브젝트에 추가
            GameObject instantiatedWallTile = 
                Instantiate(selectedWallTile, transform.position + new Vector3(wallXPos, wallYPos, 0), selectedWallTile.transform.rotation);
            instantiatedWallTile.transform.SetParent(instantiatedFloorTile.transform.Find("Wall")); // Wall 오브젝트의 자식으로 설정

            // 생성된 벽의 Bounds를 구해서 wallBoundsList에 추가
            Renderer[] renderers = instantiatedWallTile.GetComponentsInChildren<Renderer>();
            if (renderers.Length > 0 )
            {
                Bounds combinedBounds = renderers[0].bounds;
                foreach (var r in renderers)
                {
                    // Encapsulate: Bounds를 합침
                    combinedBounds.Encapsulate(r.bounds); // 여러 렌더러의 Bounds를 합쳐서 하나의 큰 Bounds 생성
                }
                wallBoundsList.Add(combinedBounds); // 생성된 벽의 Bounds를 리스트에 추가
            }
        }


        // 부품을 랜덤하게 생성
        for (int i = 0; i < ItemObstacleCount; i++)
        {
            // 부품을 랜덤하게 선택
            GameObject selectedItemObstacle = itemObstacles[Random.Range(0, itemObstacles.Count)];
            Renderer itemRenderer = selectedItemObstacle.GetComponent<Renderer>();

            Bounds itemBounds = itemRenderer.bounds; // 부품의 Bounds를 구함
            Vector3 spawnPos;
            int maxAttempts = 10; // 최대 시도 횟수
            int attempt = 0;
            bool isOverlapping;

            // 부품이 벽이나 다른 아이템 장애물과 겹치지 않도록 위치를 찾음
            do
            {
                float itemXPos = Random.Range(-5f, 5f); 
                float itemYPos = Random.Range(-9f, 24f);
                spawnPos = transform.position + new Vector3(itemXPos, itemYPos, 0);

                // 부품의 새로운 Bounds를 생성
                Bounds newItemBounds = new Bounds(spawnPos, itemBounds.size);

                // 새로운 위치가 벽이나 다른 부품과 겹치는지 체크
                isOverlapping = wallBoundsList.Any(wallBounds => wallBounds.Intersects(newItemBounds)) || // Any: 조건에 맞는 요소가 하나라도 있는지 확인
                                 itemObstacleBoundsList.Any(itemBounds => itemBounds.Intersects(newItemBounds)); // Intersects: 두 Bounds가 겹치는지 확인

                attempt++;
                if (attempt >= maxAttempts) break; // 시도 횟수 초과 시 중단

            } while (isOverlapping);

            // 겹치지 않으면 아이템 장애물 생성
            if (!isOverlapping)
            {
                GameObject instantiatedItemObstacle = Instantiate(selectedItemObstacle, spawnPos, Quaternion.identity);
                instantiatedItemObstacle.transform.SetParent(instantiatedFloorTile.transform.Find("ItemObstacle"));

                // 생성된 아이템 장애물의 Bounds를 추가
                itemObstacleBoundsList.Add(new Bounds(spawnPos, itemBounds.size));
            }     
        }
        

        for(int i = 0; i < enemyCount; i ++)
        {
            int randomMonster = Random.Range(0, 2);
            GameObject monster = null;

            if (randomMonster == 0)
            {
                GameObject flyEnemy = MonsterManager.Instance.FlyMonsterFromPool();
                monster = flyEnemy;
            }
            else
            {
                GameObject greenEnemy = MonsterManager.Instance.GreenMonsterFromPool();
                monster = greenEnemy;
            }

            Renderer monsterRenderer = monster.GetComponentInChildren<Renderer>();

            Bounds monsterBounds = monsterRenderer.bounds;
            Vector3 spawnPos;
            int maxAttempts = 10; // 최대 시도 횟수
            int attempt = 0;
            bool isOverlapping;

            do
            {
                float monsterXPos = Random.Range(-5f, 5f);
                float monsterYPos = Random.Range(-9f, 24f);

                spawnPos = new Vector3(monsterXPos, monsterYPos, 0);
                Bounds newMonsterBounds = new Bounds(spawnPos, monsterBounds.size);

                isOverlapping = wallBoundsList.Any(wallBounds => wallBounds.Intersects(newMonsterBounds));

                attempt++;
                if (attempt >= maxAttempts) break; // 시도 횟수 초과 시 중단

            } while (isOverlapping);

            // 겹치지 않으면 아이템 장애물 생성
            if (!isOverlapping)
            {
                monster.transform.position = spawnPos;
                // 생성된 아이템 장애물의 Bounds를 추가
                enemyBoundsList.Add(new Bounds(spawnPos, monsterBounds.size));
            }
        }

        return instantiatedFloorTile;
    }  
}


