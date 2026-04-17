using UnityEngine;
using System.Collections;

public class BreakOnHit : MonoBehaviour
{
    public float force = 500f;
    public float delay = 3f;

    // ▼タグ指定（例："Bullet"）
    public string targetTag = "Bool";

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

        gameObject.tag = "Break";

        // ▼Triggerだけ無効化（破片は残す）
        if (parentCol != null)
        {
            parentCol.enabled = false;
        }

        foreach (Rigidbody rb in bodies)
        {


            rb.gameObject.tag = "Break"; 


            rb.isKinematic = false;
            rb.useGravity = true;
           
            Collider col = rb.GetComponent<Collider>();
            if (col != null)
            {
                col.enabled = false;
            }
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