using UnityEngine;
using System.Collections.Generic;

public class FloorSpawner : MonoBehaviour
{
    [Header("最初に出す床（固定）")]
    public GameObject[] startPrefabs;

    [Header("通常床（ランダム）")]
    public GameObject[] floorPrefabs;

    public Transform player;

    public int startSpawnCount = 5;
    public float floorLength = 10f;
    public float deleteDistance = 30f;

    private float nextZ;
    private Queue<GameObject> floors = new Queue<GameObject>();

    private int startIndex = 0;

    void Start()
    {
        nextZ = transform.position.z;
        InitialSpawn();
    }

    void Update()
    {
        SpawnCheck();
        DeleteCheck();
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
        float distanceAhead = startSpawnCount * floorLength;

        if (player.position.z + distanceAhead > nextZ)
        {
            SpawnFloor();
        }
    }

    void SpawnFloor()
    {
        GameObject prefab;

        // ★ 最初は固定Prefabを順番に使う
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