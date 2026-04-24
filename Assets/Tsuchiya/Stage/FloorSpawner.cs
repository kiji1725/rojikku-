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

    [Header("削除距離")]
    public float deleteDistance = 30f;

    [Header("風景生成")]
    public SideObjectSpawner sideSpawner;

    private Queue<GameObject> floors = new Queue<GameObject>();
    private float nextZ = 0f;

    void Start()
    {
        for (int i = 0; i < startSpawnCount; i++)
        {
            SpawnStartFloor();
        }

        for (int i = 0; i < aheadCount; i++)
        {
            SpawnRandomFloor();
        }
    }

    void Update()
    {
        if (player.position.z + (aheadCount * floorLength) > nextZ)
        {
            SpawnRandomFloor();
        }

        if (floors.Count > 0)
        {
            GameObject first = floors.Peek();

            if (player.position.z - first.transform.position.z > deleteDistance)
            {
                Destroy(floors.Dequeue()); // 子も一緒に消える
            }
        }
    }

    void SpawnStartFloor()
    {
        GameObject prefab = startPrefabs[Random.Range(0, startPrefabs.Length)];
        Spawn(prefab);
    }

    void SpawnRandomFloor()
    {
        GameObject prefab = floorPrefabs[Random.Range(0, floorPrefabs.Length)];
        Spawn(prefab);
    }

    void Spawn(GameObject prefab)
    {
        Vector3 spawnPos = new Vector3(0, 0, nextZ);

        GameObject obj = Instantiate(prefab, spawnPos, Quaternion.identity);
        floors.Enqueue(obj);

        // ▼ここが超重要
        if (sideSpawner != null)
        {
            sideSpawner.SpawnSideObjects(spawnPos, obj.transform);
        }

        nextZ += floorLength;
    }
}