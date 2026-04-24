using UnityEngine;
using System.Collections.Generic;

public class FloorSpawner : MonoBehaviour
{
    [Header("最初に出す床（固定）")]
    public GameObject[] startPrefabs;

    [Header("通常床（ランダム）")]
    public GameObject[] floorPrefabs;

    public Transform player;

    [Header("生成設定")]
    public int startSpawnCount = 5;
    public int aheadCount = 7;
    public float floorLength = 10f;
    public float deleteDistance = 30f;

    private float nextZ;
    private Queue<GameObject> floors = new Queue<GameObject>();
    private int startIndex = 0;

    // ★ デバッグ用
    private int totalSpawned = 0;

    void Start()
    {
        nextZ = transform.position.z;
        InitialSpawn();
    }

    void Update()
    {
        SpawnCheck();
        DeleteCheck();

        // ★ 毎フレーム表示（軽くしたいなら消してOK）
        Debug.Log($"現在の床数: {floors.Count} / 累計生成: {totalSpawned}");
    }

    void InitialSpawn()
    {
        for (int i = 0; i < startSpawnCount; i++)
        {
            SpawnFloor();
        }
    }

    void SpawnCheck()
    {
        float distanceAhead = aheadCount * floorLength;

        if (player.position.z + distanceAhead > nextZ)
        {
            SpawnFloor();
        }
    }

    void SpawnFloor()
    {
        GameObject prefab;

        if (startIndex < startPrefabs.Length)
        {
            prefab = startPrefabs[startIndex];
            startIndex++;
        }
        else
        {
            prefab = GetRandomPrefab();
        }

        Vector3 spawnPos = new Vector3(
            transform.position.x,
            transform.position.y,
            nextZ
        );

        GameObject floor = Instantiate(prefab, spawnPos, Quaternion.identity);

        floors.Enqueue(floor);
        nextZ += floorLength;

        // ★ カウント
        totalSpawned++;
    }

    GameObject GetRandomPrefab()
    {
        int index = Random.Range(0, floorPrefabs.Length);
        return floorPrefabs[index];
    }

    void DeleteCheck()
    {
        if (floors.Count == 0) return;

        GameObject first = floors.Peek();

        float distance = player.position.z - first.transform.position.z;

        if (distance > deleteDistance)
        {
            Destroy(floors.Dequeue());
        }
    }
}