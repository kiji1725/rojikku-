using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

//弾はRigidbody(Is Kinematicはオフ) ,Collider(Edit ColliderとProvides Contactsはオフ)必須
//targets にドラッグ

// ▼重なったら爆散（Trigger版）
public class BreakOnHit : MonoBehaviour
{
    public float force = 500f;
    public float delay = 3f;

    // ▼ここに当たり判定させたいオブジェクト
    public GameObject[] targets;

    Rigidbody[] bodies;
    Collider[] cols;

    bool isBroken = false;

    void Start()
    {
        bodies = GetComponentsInChildren<Rigidbody>();
        cols = GetComponentsInChildren<Collider>();

        // ▼最初は完全固定（崩壊防止）
        foreach (Rigidbody rb in bodies)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (isBroken) return;

        foreach (GameObject target in targets)
        {
            // ▼子オブジェクトでも反応するようにする
            if (other.gameObject == target || other.transform.root == target.transform)
            {
                Break(other.transform.position);
                return;
            }
        }
    }

    void Break(Vector3 hitPoint)
    {
        isBroken = true;

        // ▼全部のColliderを無効化（再判定防止）
        foreach (Collider c in cols)
        {
            c.enabled = false;
        }

        // ▼物理ON＋爆発
        foreach (Rigidbody rb in bodies)
        {
            rb.isKinematic = false;
            rb.useGravity = true;

            Vector3 dir = (rb.worldCenterOfMass - hitPoint).normalized;
            rb.AddForce(dir * force, ForceMode.Impulse);
        }

        StartCoroutine(DeleteAfterDelay());
    }

    IEnumerator DeleteAfterDelay()
    {
        yield return new WaitForSeconds(delay);

        foreach (Rigidbody rb in bodies)
        {
            if (rb != null)
            {
                Destroy(rb.gameObject);
            }
        }
    }
}