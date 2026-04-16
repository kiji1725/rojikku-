using UnityEngine;
using System.Collections;

public class BreakOnHit : MonoBehaviour
{
    public float force = 500f;
    public float delay = 3f;

    // ▼タグ指定（例："Bullet"）
    public string targetTag = "Bullet";

    Rigidbody[] bodies;
    Collider parentCol;

    bool isBroken = false;

    void Start()
    {
        bodies = GetComponentsInChildren<Rigidbody>();
        parentCol = GetComponent<Collider>();

        // ▼最初は固定
        foreach (Rigidbody rb in bodies)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (isBroken) return;

        // ▼タグで判定（これだけでOK）
        if (other.CompareTag(targetTag))
        {
            Break(other.transform.position);
        }
    }

    void Break(Vector3 hitPoint)
    {
        isBroken = true;

        // ▼Triggerだけ無効化（破片は残す）
        if (parentCol != null)
        {
            parentCol.enabled = false;
        }

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