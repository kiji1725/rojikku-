using UnityEngine;
using System.Collections.Generic;

public class FloorSpawner : MonoBehaviour
{
    [Header("最初に出す床（固定）")]
    public GameObject[] startPrefabs;

    [Header("通常床（ランダム）")]
    public GameObject[] floorPrefabs;

    [Header("Stage全体の親")]
    public Transform stageRoot;

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
            SpawnStartFloor();

        for (int i = 0; i < aheadCount; i++)
            SpawnRandomFloor();
    }

    void Update()
    {
        HandleSpawn();
        HandleDelete();
    }

    void HandleSpawn()
    {
        float referenceZ = -stageRoot.position.z;

        if (referenceZ + (aheadCount * floorLength) > nextZ)
        {
            SpawnRandomFloor();
        }
    }

    void HandleDelete()
    {
        if (floors.Count == 0) return;

        GameObject first = floors.Peek();

        float referenceZ = -stageRoot.position.z;

        if (referenceZ - first.transform.position.z > deleteDistance)
        {
            Destroy(floors.Dequeue());
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

        if (sideSpawner != null)
        {
            sideSpawner.SpawnSideObjects(spawnPos, obj.transform);
        }

        nextZ += floorLength;
    }
}