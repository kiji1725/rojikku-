using UnityEngine;
using System.Collections.Generic;

public class StageManager : MonoBehaviour
{
    public GameObject[] stagePrefabs;

    public Transform player;   // プレイヤー
    public float stageLength = 20f;

    public int stageCount = 5;

    private float spawnZ = 0f;
    private float nextSpawnZ = 0f;

    private Queue<GameObject> stages = new Queue<GameObject>();

    void Start()
    {
        for (int i = 0; i < stageCount; i++)
        {
            SpawnStage();
        }

        nextSpawnZ = stageLength * 2;
    }

    void Update()
    {
        // ▼プレイヤーが一定距離進んだら生成
        if (player.position.z > nextSpawnZ)
        {
            SpawnStage();
            nextSpawnZ += stageLength;
        }
    }

    void SpawnStage()
    {
        int index = Random.Range(0, stagePrefabs.Length);

        Vector3 pos = new Vector3(0, 0, spawnZ);

        GameObject stage = Instantiate(stagePrefabs[index], pos, Quaternion.identity);

        stages.Enqueue(stage);

        spawnZ += stageLength;

        if (stages.Count > stageCount)
        {
            Destroy(stages.Dequeue());
        }
    }
}