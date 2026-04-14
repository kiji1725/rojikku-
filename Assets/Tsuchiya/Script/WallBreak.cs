using UnityEngine;
using System.Collections;

// 壁を爆散させる処理クラス
public class WallBreak : MonoBehaviour
{
    // ▼吹っ飛ばす力の強さ
    public float force = 500f;

    // ▼削除までの待機時間（秒）
    public float delay = 3f;

    // ▼このオブジェクト配下のRigidbodyを取得
    Rigidbody[] bodies;

    // ▼当たり判定（Collider）
    Collider col;

    void Start()
    {
        // 子オブジェクトも含めて取得
        bodies = GetComponentsInChildren<Rigidbody>();

        // 自分のCollider取得
        col = GetComponent<Collider>();
    }

    void Update()
    {
        // ▼スペースキーで発動（デバッグ用）
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // ▼当たり判定をオフにする
            if (col != null)
            {
                col.enabled = false;
            }

            StartCoroutine(ExplodeAndDelete());
        }
    }

    // ▼壁を爆散させて一定時間後に削除する処理
    IEnumerator ExplodeAndDelete()
    {
        // ▼このオブジェクト配下のオブジェクトだけ吹っ飛ばす
        foreach (Rigidbody rb in bodies)
        {
            if (rb == null) continue;

            // ▼ランダム方向＋少し上方向
            Vector3 dir = (Random.onUnitSphere + Vector3.up).normalized;

            // ▼力を加える
            rb.AddForce(dir * force, ForceMode.Impulse);
        }

        Debug.Log("爆散した");

        // ▼待機
        yield return new WaitForSeconds(delay);

        // ▼削除（子オブジェクトのみ）
        foreach (Rigidbody rb in bodies)
        {
            if (rb != null)
            {
                Destroy(rb.gameObject);
            }
        }

        Debug.Log("削除完了");
    }
}