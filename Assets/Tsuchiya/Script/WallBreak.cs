using UnityEngine;

public class WallBreak : MonoBehaviour
{
    public float force = 500f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ExplodeAll();
        }
    }

    void ExplodeAll()
    {
        // シーン内の全Rigidbody取得
        Rigidbody[] bodies = FindObjectsOfType<Rigidbody>();

        foreach (Rigidbody rb in bodies)
        {
            // 自分（カメラ）は除外
            if (rb.gameObject == this.gameObject) continue;

            // 吹っ飛ばす方向（ランダム＋上方向）
            Vector3 dir = (Random.onUnitSphere + Vector3.up).normalized;

            rb.AddForce(dir * force, ForceMode.Impulse);
        }

        Debug.Log("全て爆散");
    }
}