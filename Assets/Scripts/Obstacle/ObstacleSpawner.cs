using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

[System.Serializable]
public class TileGroup
{
    public GameObject floorTile; // 바닥 타일
    public List<GameObject> wallTiles; // 벽 타일
    public List<GameObject> itemObstacle; // 아이템 장애물
}

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField]
    private List<TileGroup> tileGroups;

    private Dictionary<GameObject, List<GameObject>> tileToWalls = new Dictionary<GameObject, List<GameObject>>();
    private Dictionary<GameObject, List<GameObject>> tileToItemObstacle = new Dictionary<GameObject, List<GameObject>>();

    private List<Bounds> wallBoundsList = new List<Bounds>(); // 벽의 Bounds 리스트
    private List<Bounds> itemObstacleBoundsList = new List<Bounds>(); // 아이템 장애물의 Bounds 리스트

    // Start is called before the first frame update
    void Start()
    {
        InitTileDictionary();
        CreateFloorTiles(1, 3, 100);
    }

    // Update is called once per frame
    void Update()
    {
        
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

    // 주어진 stage에 맞는 바닥 타일을 생성하고, 해당 타일에 벽 타일과 아이템 장애물을 배치
    void CreateFloorTiles(int stage, int wallTileCount, int ItemObstacleCount)
    {
        // 특정 바닥 타일을 선택해서 생성
        GameObject selectedFloorTile = tileToWalls.Keys.ElementAt(stage - 1); // 1 ~ 5 선택 후 생성
        List<GameObject> wallTiles = tileToWalls[selectedFloorTile];
        List<GameObject> itemObstacles = tileToItemObstacle[selectedFloorTile];

        // 선택된 바닥 타일을 생성
        GameObject instantiatedFloorTile = Instantiate(selectedFloorTile, transform.position, Quaternion.identity);
        wallBoundsList.Clear(); // 벽 Bounds 리스트 초기화
        itemObstacleBoundsList.Clear(); // 아이템 장애물 Bounds 리스트 초기화

        for (int i = 0; i < wallTileCount; i++)
        {
            GameObject selectedWallTile = wallTiles[Random.Range(0, wallTiles.Count)];
            WallObstacle wallObstacle = selectedWallTile.GetComponent<WallObstacle>();
            float wallXPos = Random.Range(wallObstacle.lowPosX, wallObstacle.highPosX);
            int wallYPos = i * 8;

            // 벽 타일을 생성
            GameObject instantiatedWallTile = 
                Instantiate(selectedWallTile, transform.position + new Vector3(wallXPos, wallYPos, 0), Quaternion.identity);
            instantiatedWallTile.transform.SetParent(instantiatedFloorTile.transform.Find("Wall")); // Wall 오브젝트의 자식으로 설정

            // 벽의 Bounds 저장
            Renderer[] renderers = instantiatedWallTile.GetComponentsInChildren<Renderer>();
            if (renderers.Length > 0 )
            {
                Bounds combinedBounds = renderers[0].bounds;
                foreach (var r in renderers)
                {
                    combinedBounds.Encapsulate(r.bounds); // 여러 렌더러를 포함하는 Bounds 생성
                }
                wallBoundsList.Add(combinedBounds);
            }
        }



        for (int i = 0; i < ItemObstacleCount; i++)
        {
            GameObject selectedItemObstacle = itemObstacles[Random.Range(0, itemObstacles.Count)];
            Renderer itemRenderer = selectedItemObstacle.GetComponent<Renderer>();

            Bounds itemBounds = itemRenderer.bounds; // 기존 Bounds 가져오기
            Vector3 spawnPos;
            int maxAttempts = 10; // 최대 시도 횟수
            int attempt = 0;
            bool isOverlapping;

            do
            {
                float itemXPos = Random.Range(-5f, 5f); // 범위 수정 가능
                float itemYPos = Random.Range(-9f, 14f);
                spawnPos = transform.position + new Vector3(itemXPos, itemYPos, 0);

                Bounds newItemBounds = new Bounds(spawnPos, itemBounds.size); // 새로운 Bounds 생성

                isOverlapping = wallBoundsList.Any(wallBounds => wallBounds.Intersects(newItemBounds)) ||
                                 itemObstacleBoundsList.Any(itemBounds => itemBounds.Intersects(newItemBounds));

                attempt++;
                if (attempt >= maxAttempts) break; // 시도 횟수 초과 시 중단

            } while (isOverlapping);

            if (!isOverlapping)
            {
                GameObject instantiatedItemObstacle = Instantiate(selectedItemObstacle, spawnPos, Quaternion.identity);
                instantiatedItemObstacle.transform.SetParent(instantiatedFloorTile.transform.Find("ItemObstacle"));

                // 아이템 장애물의 Bounds 추가
                itemObstacleBoundsList.Add(new Bounds(spawnPos, itemBounds.size));
            }
        }
    }  
}



