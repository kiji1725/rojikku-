using UnityEngine;
using System.Collections;

public class BreakOnHit : MonoBehaviour
{
    // ▼爆発の強さ
    public float force = 500f;

    // ▼何秒後に破片を削除するか
    public float delay = 3f;

    // ▼当たり判定させるタグ（このタグのオブジェクトが当たると発動）
    public string targetTag = "Bool";

    // ▼何個に減らすか（軽量化用：残す破片の最大数）
    public int maxFragments = 8;

    // ▼子オブジェクトのRigidbody一覧
    Rigidbody[] bodies;

    // ▼親オブジェクトのCollider（破壊後に無効化する）
    Collider parentCol;

    // ▼すでに破壊済みかどうか（多重実行防止）
    bool isBroken = false;

    void Start()
    {
        // ▼子オブジェクトのRigidbodyをすべて取得
        bodies = GetComponentsInChildren<Rigidbody>();

        // ▼親のColliderを取得
        parentCol = GetComponent<Collider>();

        // ▼最初はすべての破片の物理を停止
        foreach (Rigidbody rb in bodies)
        {
            rb.isKinematic = true;   // 物理演算OFF
            rb.useGravity = false;   // 重力OFF
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // ▼すでに壊れていたら何もしない
        if (isBroken) return;

        // ▼指定タグのオブジェクトと当たったら破壊
        if (other.CompareTag(targetTag))
        {
            Break(other.transform.position);
        }
    }

    // ▼破壊処理本体
    void Break(Vector3 hitPoint)
    {
        isBroken = true;

        // ▼親のColliderを無効化（連続ヒット防止）
        if (parentCol != null)
        {
            parentCol.enabled = false;
        }

        int count = 0; // ▼現在の破片数カウント

        // ▼すべての破片に対して処理
        foreach (Rigidbody rb in bodies)
        {
            // ▼上限を超えたら削除（軽量化）
            if (count >= maxFragments)
            {
                Destroy(rb.gameObject);
                continue;
            }

            // ▼残す破片は物理を有効化
            rb.isKinematic = false;
            rb.useGravity = true;

            // ▼ヒット位置から外側に向かって吹っ飛ばす
            Vector3 dir = (rb.worldCenterOfMass - hitPoint).normalized;
            rb.AddForce(dir * force, ForceMode.Impulse);

            count++;
        }

        // ▼一定時間後に削除処理
        StartCoroutine(DeleteAfterDelay());
    }

    // ▼指定時間後に破片を削除するコルーチン
    IEnumerator DeleteAfterDelay()
    {
        yield return new WaitForSeconds(delay);

        foreach (Rigidbody rb in bodies)
        {
            // ▼存在している破片だけ削除
            if (rb != null)
            {
                Destroy(rb.gameObject);
            }
        }
    }
}