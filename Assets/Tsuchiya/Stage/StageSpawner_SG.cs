using UnityEngine;
using System.Collections.Generic;

public class StageSpawner_SG : MonoBehaviour
{
    public GameObject[] stagePrefabs;
    public Transform player;

    public int initialSpawn = 5;
    public float spawnDistance = 30f;

    private Transform lastEndPoint;
    private Queue<GameObject> stages = new Queue<GameObject>();

    void Start()
    {
        // 最初のステージ
        GameObject first = Instantiate(stagePrefabs[0]);
        lastEndPoint = first.transform.Find("G");

        stages.Enqueue(first);

        // 初期生成
        for (int i = 0; i < initialSpawn; i++)
        {
            SpawnStage();
        }
    }

    void Update()
    {
        if (Vector3.Distance(player.position, lastEndPoint.position) < spawnDistance)
        {
            SpawnStage();
            DeleteOld();
        }
    }

    void SpawnStage()
    {
        GameObject prefab = stagePrefabs[Random.Range(0, stagePrefabs.Length)];
        GameObject stage = Instantiate(prefab);

        Transform start = stage.transform.Find("S");
        Transform end = stage.transform.Find("G");

        // ▼Sを前のGに合わせる（これが核心）
        stage.transform.position = lastEndPoint.position - (start.position - stage.transform.position);

        // ▼回転も合わせる（ズレ防止）
        stage.transform.rotation = lastEndPoint.rotation;

        lastEndPoint = end;

        stages.Enqueue(stage);
    }

    void DeleteOld()
    {
        if (stages.Count > 7)
        {
            Destroy(stages.Dequeue());
        }
    }
}