using UnityEngine;

public class Raycast : MonoBehaviour
{

    [SerializeField] private PlayerMove player;

    [SerializeField] private float rayDistance = 100.0f;
    float rayRotate = 45;
    public float rayPosY = 0.0f;


    

    bool wallRun = false;

    void Start()
    {

        //transform.Rotate(0.0f, rayRotate, 0.0f);

    }
    void Update()
    {

        // 最終的にプレイヤーの進行方向と同じ方向にプレイヤーと同時に進むようにする
        transform.position = new Vector3(player.PlayerPos.x, player.PlayerPos.y + rayPosY, player.PlayerPos.z);


        // Ray構造体を作成
        Ray ray = new Ray(transform.position, transform.forward);

        // RaycastHit には当たったオブジェクトの情報が入る
        RaycastHit hitLeft;
        RaycastHit hitRight;

        // 右前方向
        Vector3 rightFront = (transform.forward + transform.right).normalized;
        // 左前方向
        Vector3 leftFront = (transform.forward - transform.right).normalized;

        // transform.position から transform.forward に向かってRayを飛ばす
        // Rayが当たったところの座標からプレイヤーに壁走りができることを伝えることができるようにする

        // 右側のRay
        if (Physics.Raycast(transform.position, rightFront, out hitRight, rayDistance))
        {
            //Debug.Log("ヒットまでの距離: " + hit.distance);
            Debug.Log("右");
            Debug.Log("当たった相手: " + hitRight.collider.name);
            Debug.Log("当たった場所 : " + hitRight.point);

        }
        // 左側のRay
        if (Physics.Raycast(transform.position, leftFront, out hitLeft, rayDistance))
        {
            //Debug.Log("ヒットまでの距離: " + hit.distance);
            Debug.Log("左");
            Debug.Log("当たった相手: " + hitLeft.collider.name);
            Debug.Log("当たった場所 : " + hitLeft.point);

        }


        if (!Physics.Raycast(transform.position, leftFront, out hitLeft, rayDistance))
        {
            Debug.Log("当たってない");
        }

        // 赤い線と青い線を描画
        Debug.DrawRay(ray.origin, leftFront * rayDistance, Color.red);
        Debug.DrawRay(ray.origin, rightFront * rayDistance, Color.blue);

        //if ()
        //{

        //}


    }// update

    // Rayが当たった座標で分岐してこのフラグをtrueにすると壁走りができるようになる
    public bool IsWallRun { get { return wallRun;  } }




}
