using UnityEngine;

public class Raycast_test : MonoBehaviour
{
    // Rayを飛ばす距離
    [SerializeField] private float rayDistance = 100f;

    private void Update()
    {
        // Ray構造体を作成
        Ray ray = new Ray(transform.position, transform.forward);
        
        // RaycastHit には当たったオブジェクトの情報が入る
        RaycastHit hit;

        // transform.position から transform.forward に向かってRayを飛ばす
        if (Physics.Raycast(transform.position, transform.forward, out hit, rayDistance))
        {
            Debug.Log("当たった相手: " + hit.collider.name);
            Debug.Log("ヒットまでの距離: " + hit.distance);
        }

        // 赤い線をシーンビューで描画
        Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.red);


    }
}
