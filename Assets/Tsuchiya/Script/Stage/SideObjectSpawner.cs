using UnityEngine;

public class SideObjectSpawner : MonoBehaviour
{
    [Header("生成するオブジェクト")]
    public GameObject[] sidePrefabs;

    [Header("左右の距離")]
    public float sideOffset = 10f;

    [Header("前後のランダム幅")]
    public float zRandomRange = 2f;

    [Header("生成確率（0～1）")]
    [Range(0f, 1f)]
    public float spawnChance = 0.8f;

    public void SpawnSideObjects(Vector3 basePos, Transform parent)
    {
        if (sidePrefabs == null || sidePrefabs.Length == 0) return;

        TrySpawn(basePos, -sideOffset, parent);
        TrySpawn(basePos, sideOffset, parent);
    }

    void TrySpawn(Vector3 basePos, float xOffset, Transform parent)
    {
        if (Random.value > spawnChance) return;

        GameObject prefab = sidePrefabs[Random.Range(0, sidePrefabs.Length)];
        if (prefab == null) return;

        Vector3 pos = new Vector3(
            basePos.x + xOffset,
            basePos.y,
            basePos.z + Random.Range(-zRandomRange, zRandomRange)
        );

        Quaternion rot = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);

        Instantiate(prefab, pos, rot, parent);
    }
}