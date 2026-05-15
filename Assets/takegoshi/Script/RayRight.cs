using UnityEngine;

public class RayRight : MonoBehaviour
{

    [SerializeField] private PlayerMove player;

    [SerializeField] private float rayDistance = 10.0f;

    public float rayPosY = 0.0f;

    public bool wallRunRight = false;

    public float frontWeight = 1.0f;
    
    void Update()
    {

        // 最終的にプレイヤーの進行方向と同じ方向にプレイヤーと同時に進むようにする
        transform.position = new Vector3(transform.position.x, rayPosY, player.PlayerPos.z + 1.0f);

        // Ray構造体を作成
        Ray ray = new(transform.position, transform.forward);

        // 右前方向
        Vector3 rightFront = (transform.forward + transform.right * frontWeight).normalized;
        
        // transform.position から transform.forward に向かってRayを飛ばす
        // Rayが当たったところの座標からプレイヤーに壁走りができることを伝えることができるようにする

        // 右側のRay
        if (Physics.Raycast(transform.position, rightFront, out RaycastHit hitRight, rayDistance))
        {
            //Debug.Log("ヒットまでの距離: " + hit.distance);
            wallRunRight = true;
            
        }

        // 右が当たってないとき
        if (!Physics.Raycast(transform.position, rightFront, out _, rayDistance))
        {
            wallRunRight = false;
            
        }

        // 青い線を描画
        Debug.DrawRay(ray.origin, rightFront * rayDistance, Color.blue);

    }

    // Rayが当たった座標で分岐してこのフラグをtrueにすると壁走りができるようにする
    public bool IsWallRunRight { get { return wallRunRight; } }
   
}
