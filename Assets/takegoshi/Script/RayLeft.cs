using UnityEngine;

public class RayLeft : MonoBehaviour
{
    [SerializeField] private PlayerMove player;

    [SerializeField] private float rayDistance = 10.0f;

    public bool wallRunLeft = false;

    public float frontWeight = 1.0f;
    
    void Update()
    {

        // 最終的にプレイヤーの進行方向と同じ方向にプレイヤーと同時に進むようにする
        //transform.position = new Vector3(transform.position.x, rayPosY, player.PlayerPos.z + 1.0f);

        // Ray構造体を作成
        Ray ray = new(transform.position, transform.forward);

        // 左前方向
        Vector3 leftFront = (transform.forward - transform.right * frontWeight).normalized;

        // transform.position から transform.forward に向かってRayを飛ばす
        // Rayが当たったところの座標からプレイヤーに壁走りができることを伝えることができるようにする

        // 左側のRay
        if (Physics.Raycast(transform.position, leftFront, out RaycastHit hitLeft, rayDistance))
        {
            wallRunLeft = true;

            Debug.Log("左当たった場所 : " + hitLeft.point);

        }

        // 左が当たってないとき
        if (!Physics.Raycast(transform.position, leftFront, out _, rayDistance))
        {

            wallRunLeft = false;
            Debug.Log("左当たってない");

        }

        // 赤い線を描画
        Debug.DrawRay(ray.origin, leftFront * rayDistance, Color.red);

    }

    // Rayが当たった座標で分岐してこのフラグをtrueにすると壁走りができるようにする
    public bool IsWallRunLeft { get { return wallRunLeft; } }

}
