using UnityEngine;

public class Raycast : MonoBehaviour
{

    [SerializeField] private PlayerMove player;

    [SerializeField] private float rayDistance = 10.0f;

    public float rayPosY = 0.0f;

    

    Vector3 hitPosRight = Vector3.zero;
    Vector3 hitPosLeft = Vector3.zero;

    

    bool wallRunRight = false;
    bool wallRunLeft = false;

    public   float frontWeight = 1.0f;

    void Start()
    {

        //transform.Rotate(0.0f, rayRotate, 0.0f);

    }
    void Update()
    {

        // 最終的にプレイヤーの進行方向と同じ方向にプレイヤーと同時に進むようにする
        transform.position = new Vector3(transform.position.x, rayPosY, player.PlayerPos.z + 1.0f);

        // Ray構造体を作成
        Ray ray = new Ray(transform.position, transform.forward);

        // RaycastHit には当たったオブジェクトの情報が入る
        RaycastHit hitLeft;
        RaycastHit hitRight;

        // 右前方向
        Vector3 rightFront = (transform.forward + transform.right * frontWeight).normalized;
        // 左前方向
        Vector3 leftFront = (transform.forward - transform.right * frontWeight).normalized;

        // transform.position から transform.forward に向かってRayを飛ばす
        // Rayが当たったところの座標からプレイヤーに壁走りができることを伝えることができるようにする

        // 左側のRay
        if (Physics.Raycast(transform.position, leftFront, out hitLeft, rayDistance))
        {
            //Debug.Log("ヒットまでの距離: " + hit.distance);
            wallRunLeft = true;

            Debug.Log("左");
            Debug.Log("左当たった場所 : " + hitLeft.point);
            
            hitPosLeft = hitLeft.point;
        }
        // 右側のRay
        if (Physics.Raycast(transform.position, rightFront, out hitRight, rayDistance))
        {
            //Debug.Log("ヒットまでの距離: " + hit.distance);
            wallRunRight = true;
            Debug.Log("右");
            Debug.Log("右当たった場所 : " + hitRight.point);

            hitPosRight = hitRight.point;
        }

        // 左
        if (!Physics.Raycast(transform.position, leftFront, out hitLeft, rayDistance))
        {
            
            wallRunLeft = false;
            Debug.Log("左当たってない");
        
            hitPosLeft = Vector3.zero;
        
        }

        // 右
        if (Physics.Raycast(transform.position, rightFront, out hitRight, rayDistance))
        {
            wallRunRight = false;
            Debug.Log("右当たってない");

            hitPosLeft = Vector3.zero;
        }
        





        // 赤い線と青い線を描画
        Debug.DrawRay(ray.origin, leftFront * rayDistance, Color.red);
        Debug.DrawRay(ray.origin, rightFront * rayDistance, Color.blue);



    }// update

    // Rayが当たった座標で分岐してこのフラグをtrueにすると壁走りができるようにする
    public bool IsWallRunRight { get { return wallRunRight;  } }
    public bool IsWallRunLeft { get { return wallRunLeft; } }





}
