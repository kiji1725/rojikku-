using UnityEngine;
using System.Collections;

// 壁を爆散させる処理クラス
public class WallBreak : MonoBehaviour
{
    // ▼吹っ飛ばす力の強さ
    public float force = 500f;

    // ▼削除までの待機時間（秒）
    public float delay = 3f; // 3秒

    void Update()
    {
        // ▼発動条件
        // 現在：スペースキーで発動（デバッグ用）
        // 将来：プレイヤーの銃弾が当たったときに発動する仕様に変更予定
        // TODO: 銃のヒット判定からこの処理を呼び出すようにする
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(ExplodeAndDelete());
        }
    }

    // ▼壁を爆散させて一定時間後に削除する処理
    IEnumerator ExplodeAndDelete()
    {
        // ▼シーン内のすべてのRigidbodyを取得
        Rigidbody[] bodies = FindObjectsOfType<Rigidbody>();

        // ▼まずは周囲のオブジェクトを吹っ飛ばす
        foreach (Rigidbody rb in bodies)
        {
            // 自分自身は除外
            if (rb.gameObject == this.gameObject) continue;

            // ▼ランダム方向＋少し上方向に力を加える
            Vector3 dir = (Random.onUnitSphere + Vector3.up).normalized;

            // ▼力を加えて吹っ飛ばす
            rb.AddForce(dir * force, ForceMode.Impulse);
        }

        Debug.Log("爆散した");

        // ▼指定時間待機
        yield return new WaitForSeconds(delay);

        // ▼すべてのオブジェクトを削除
        foreach (Rigidbody rb in bodies)
        {
            if (rb != null)
            {
                Destroy(rb.gameObject);
            }
        }

        Debug.Log("3秒後に全削除");
    }
}