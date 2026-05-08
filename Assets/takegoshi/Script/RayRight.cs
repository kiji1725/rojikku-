using UnityEngine;

public class RayRight : MonoBehaviour
{

    [SerializeField] private PlayerMove player;

    [SerializeField] private float rayDistance = 10.0f;

    public float rayPosY = 0.0f;

    Vector3 hitPosRight = Vector3.zero;

    public bool wallRunRight = false;

    public float frontWeight = 1.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        // 最終的にプレイヤーの進行方向と同じ方向にプレイヤーと同時に進むようにする
        transform.position = new Vector3(transform.position.x, rayPosY, player.PlayerPos.z + 1.0f);

        // Ray構造体を作成
        Ray ray = new(transform.position, transform.forward);

        // RaycastHit には当たったオブジェクトの情報が入る

        RaycastHit hitRight;

        // 右前方向
        Vector3 rightFront = (transform.forward + transform.right * frontWeight).normalized;
        
        // transform.position から transform.forward に向かってRayを飛ばす
        // Rayが当たったところの座標からプレイヤーに壁走りができることを伝えることができるようにする


        // 右側のRay
        if (Physics.Raycast(transform.position, rightFront, out hitRight, rayDistance))
        {
            //Debug.Log("ヒットまでの距離: " + hit.distance);
            wallRunRight = true;
            Debug.Log("右");
            Debug.Log("右当たった場所 : " + hitRight.point);

            hitPosRight = hitRight.point;
        }


        // 右が当たってないとき
        if (!Physics.Raycast(transform.position, rightFront, out hitRight, rayDistance))
        {
            wallRunRight = false;
            Debug.Log("右当たってない");

            hitPosRight = Vector3.zero;
        }






        // 青い線を描画
        Debug.DrawRay(ray.origin, rightFront * rayDistance, Color.blue);



    }// update

    // Rayが当たった座標で分岐してこのフラグをtrueにすると壁走りができるようにする
    public bool IsWallRunRight { get { return wallRunRight; } }
   



}
