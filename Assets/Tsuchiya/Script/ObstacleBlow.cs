using UnityEngine;

public class ObstacleBlow : MonoBehaviour
{
    public float radius = 10f;
    public float power = 10f;

    // ?? ここで方向を調整できる
    public Vector3 direction = new Vector3(1f, 0.3f, 0f);

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("スペース押された");
            BlowObjects();
        }
    }

    void BlowObjects()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);

        Debug.Log("当たった数: " + hitColliders.Length);

        foreach (Collider col in hitColliders)
        {
            Rigidbody rb = col.GetComponent<Rigidbody>();

            if (rb != null)
            {
                // ?? 正規化して方向を安定させる
                Vector3 dir = direction.normalized;

                rb.AddForce(dir * power, ForceMode.Impulse);

                Debug.Log("方向ノックバック: " + col.name);

                Destroy(col.gameObject, 4f);
            }
        }
    }
}