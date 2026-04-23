using UnityEngine;

public class Raycast : MonoBehaviour
{

    [SerializeField] private float rayDistance = 100.0f;


    void Start()
    {
        
    }
    void Update()
    {
        // Ray構造体を作成
        Ray ray = new Ray(transform.position, transform.forward);

        // RaycastHit には当たったオブジェクトの情報が入る
        RaycastHit hit;

        // transform.position から transform.forward に向かってRayを飛ばす
        // Rayが当たったところの座標からプレイヤーに壁走りができることを伝えることができるようにする
        if (Physics.Raycast(transform.position, transform.forward, out hit, rayDistance))
        {
            //Debug.Log("ヒットまでの距離: " + hit.distance);

            Debug.Log("当たった相手: " + hit.collider.name);
            Debug.Log("当たった場所 : " + hit.point);

            //hit.point
            
        }

        // 赤い線をシーンビューで描画
        Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.red);

        






    }
}
