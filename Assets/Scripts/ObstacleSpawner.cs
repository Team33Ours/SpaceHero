using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

[System.Serializable]
public class TileGroup
{
    public GameObject floorTile;
    public List<GameObject> wallTiles;
}

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField]
    private List<TileGroup> tileGroups;

    private Dictionary<GameObject, List<GameObject>> tileToWalls = new Dictionary<GameObject, List<GameObject>>();

    // Start is called before the first frame update
    void Start()
    {
        InitTileDictionary();
        CreateFloorTiles(1, 3);
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

            tileToWalls[group.floorTile] = new List<GameObject>(group.wallTiles);
        }
    }

    void CreateFloorTiles(int stage, int wallTileCount)
    {
        // 특정 바닥 타일을 선택해서 생성
        GameObject selectedFloorTile = tileToWalls.Keys.ElementAt(stage - 1); // 1 ~ 5 선택 후 생성
        List<GameObject> wallTiles = tileToWalls[selectedFloorTile];

        // 선택된 바닥 타일을 생성
        GameObject instantiatedFloorTile = Instantiate(selectedFloorTile, transform.position, Quaternion.identity);
        instantiatedFloorTile.transform.SetParent(this.transform); // 부모 설정

        for (int i = 0; i < wallTileCount; i++)
        {
            int wallTileIndex = Random.Range(0, wallTiles.Count);
            GameObject selectedWallTile = wallTiles[wallTileIndex];
            WallObstacle wallObstacle = selectedWallTile.GetComponent<WallObstacle>();
            float wallXPos = Random.Range(wallObstacle.lowPosX, wallObstacle.highPosX);
            int wallYPos = i * 8;
            // 벽 타일을 생성
            GameObject instantiatedWallTile = 
                Instantiate(selectedWallTile, transform.position + new Vector3(wallXPos, wallYPos, 0), Quaternion.identity);
            instantiatedWallTile.transform.SetParent(instantiatedFloorTile.transform.Find("Wall")); // Wall 오브젝트의 자식으로 설정
        }
    }
}
