using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;


/// <summary>
/// ������ ������Ʈ�� �׷����� ��Ÿ���� Ŭ����
/// 2025.02.24.�Ѹ���
/// </summary>
[System.Serializable]
public class TileGroup
{
    public GameObject floorTile; // �ٴ� Ÿ��
    public List<GameObject> wallTiles; // �� Ÿ��
    public List<GameObject> itemObstacle; // ������ ��ֹ�
}


/// <summary>
/// ������Ʈ���� ������ ��ġ�� ����ϴ� Ŭ����
/// �ߺ��� ���̳� ������ ��ֹ��� �������� �ʵ��� Bounds�� �̿��Ͽ� ��ġ�� �˻�
/// 2025.02.24.�Ѹ���
/// </summary>
public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField]
    private List<TileGroup> tileGroups;

    private Dictionary<GameObject, List<GameObject>> tileToWalls = new Dictionary<GameObject, List<GameObject>>();
    private Dictionary<GameObject, List<GameObject>> tileToItemObstacle = new Dictionary<GameObject, List<GameObject>>();

    private List<Bounds> wallBoundsList = new List<Bounds>(); // ���� Bounds ����Ʈ
    private List<Bounds> itemObstacleBoundsList = new List<Bounds>(); // ������ ��ֹ��� Bounds ����Ʈ

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
                Debug.LogWarning("�ٴ� Ÿ�� �Ǵ� �� Ÿ�� ����Ʈ�� ��� ����!");
                continue;
            }

            // �ٴ� Ÿ���� Ű��, �� Ÿ�ϰ� ������ ��ֹ� ����� ������ ����
            tileToWalls[group.floorTile] = new List<GameObject>(group.wallTiles);
            tileToItemObstacle[group.floorTile] = new List<GameObject>(group.itemObstacle);
        }
    }

    // �־��� stage�� �´� �ٴ� Ÿ���� �����ϰ�, �ش� Ÿ�Ͽ� �� Ÿ�ϰ� ������ ��ֹ��� ��ġ
    void CreateFloorTiles(int stage, int wallTileCount, int ItemObstacleCount)
    {
        // Ư�� �ٴ� Ÿ���� �����ؼ� ����
        GameObject selectedFloorTile = tileToWalls.Keys.ElementAt(stage - 1); // ���õ� �ٴ� Ÿ���� ������ (stage�� ���� Ÿ���� ����)
        List<GameObject> wallTiles = tileToWalls[selectedFloorTile]; // �ش� �ٴ� Ÿ�Ͽ� �´� �� Ÿ�� ���
        List<GameObject> itemObstacles = tileToItemObstacle[selectedFloorTile]; // �ش� �ٴ� Ÿ�Ͽ� �´� ������ ��ֹ� ���

        // ���õ� �ٴ� Ÿ���� ����
        GameObject instantiatedFloorTile = Instantiate(selectedFloorTile, transform.position, Quaternion.identity);
        wallBoundsList.Clear(); // �� Bounds ����Ʈ �ʱ�ȭ
        itemObstacleBoundsList.Clear(); // ������ ��ֹ� Bounds ����Ʈ �ʱ�ȭ


        // �� Ÿ���� �����ϰ� ����
        for (int i = 0; i < wallTileCount; i++)
        {
            GameObject selectedWallTile = wallTiles[Random.Range(0, wallTiles.Count)]; // �� Ÿ���� �����ϰ� ����
            WallObstacle wallObstacle = selectedWallTile.GetComponent<WallObstacle>(); // �� Ÿ���� WallObstacle ������Ʈ ��������
            float wallXPos = Random.Range(wallObstacle.lowPosX, wallObstacle.highPosX);
            int wallYPos = i * 8;

            // �� Ÿ���� �����ϰ� �θ� ������Ʈ�� �߰�
            GameObject instantiatedWallTile = 
                Instantiate(selectedWallTile, transform.position + new Vector3(wallXPos, wallYPos, 0), Quaternion.identity);
            instantiatedWallTile.transform.SetParent(instantiatedFloorTile.transform.Find("Wall")); // Wall ������Ʈ�� �ڽ����� ����

            // ������ ���� Bounds�� ���ؼ� wallBoundsList�� �߰�
            Renderer[] renderers = instantiatedWallTile.GetComponentsInChildren<Renderer>();
            if (renderers.Length > 0 )
            {
                Bounds combinedBounds = renderers[0].bounds;
                foreach (var r in renderers)
                {
                    combinedBounds.Encapsulate(r.bounds); // ���� �������� Bounds�� ���ļ� �ϳ��� ū Bounds ����
                }
                wallBoundsList.Add(combinedBounds); // ������ ���� Bounds�� ����Ʈ�� �߰�
            }
        }


        // ������ ��ֹ��� �����ϰ� ����
        for (int i = 0; i < ItemObstacleCount; i++)
        {
            // ������ ��ֹ��� �����ϰ� ����
            GameObject selectedItemObstacle = itemObstacles[Random.Range(0, itemObstacles.Count)];
            Renderer itemRenderer = selectedItemObstacle.GetComponent<Renderer>();

            Bounds itemBounds = itemRenderer.bounds; // ������ ��ֹ��� Bounds�� ����
            Vector3 spawnPos;
            int maxAttempts = 10; // �ִ� �õ� Ƚ��
            int attempt = 0;
            bool isOverlapping;

            // ������ ��ֹ��� ���̳� �ٸ� ������ ��ֹ��� ��ġ�� �ʵ��� ��ġ�� ã��
            do
            {
                float itemXPos = Random.Range(-5f, 5f); 
                float itemYPos = Random.Range(-9f, 14f);
                spawnPos = transform.position + new Vector3(itemXPos, itemYPos, 0);

                // ������ ��ֹ��� ���ο� Bounds�� ����
                Bounds newItemBounds = new Bounds(spawnPos, itemBounds.size);

                // ���ο� ��ġ�� ���̳� �ٸ� ������ ��ֹ��� ��ġ���� üũ
                isOverlapping = wallBoundsList.Any(wallBounds => wallBounds.Intersects(newItemBounds)) ||
                                 itemObstacleBoundsList.Any(itemBounds => itemBounds.Intersects(newItemBounds));

                attempt++;
                if (attempt >= maxAttempts) break; // �õ� Ƚ�� �ʰ� �� �ߴ�

            } while (isOverlapping);

            // ��ġ�� ������ ������ ��ֹ� ����
            if (!isOverlapping)
            {
                GameObject instantiatedItemObstacle = Instantiate(selectedItemObstacle, spawnPos, Quaternion.identity);
                instantiatedItemObstacle.transform.SetParent(instantiatedFloorTile.transform.Find("ItemObstacle"));

                // ������ ������ ��ֹ��� Bounds�� �߰�
                itemObstacleBoundsList.Add(new Bounds(spawnPos, itemBounds.size));
            }
        }
    }  
}



