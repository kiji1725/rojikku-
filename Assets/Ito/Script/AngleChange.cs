using UnityEngine;

public class AngleChange : MonoBehaviour
{
    // 角度を変えるためのAnimatorとPlayerMotionControllerをインスペクターで設定
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerMotionController motionController;
    [SerializeField] private Raycast ray;

    // string GR = "GunRun";

    // 角度の最大値とステップ値を定数として定義
    public float maxAngle = 90f;
    public float stepAngle = 45f;

    public int changeCount = 0;

    // 角度を変えるスピードをインスペクターで設定
    [SerializeField] private float rotateSpeed = 10f;

    // 目標のZ軸の角度と現在のZ軸の角度を保持する変数
    float targetZ = 0f;
    float currentZ = 0f;
    
    void Update()
    {

        

        // 走るアニメーションのときだけ角度を変える
        if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) && !motionController.JumpFlag)
        {

            // 右移動
            // 0から1
            // 1から2
            if (changeCount == 0)
            {
                // ここで１になる
                changeCount++;
                //currentZ = 45.0f;
                targetZ = 45.0f;
            }
            else if (changeCount == 1 && ray.IsWallRunRight)
            {
                // 2になる
                changeCount++;
                //currentZ = 90;
                targetZ = 90.0f;
            }
            // それ以外
            else if (changeCount < 0)
            {
                changeCount++;
                //currentZ = Mathf.Min(currentZ + stepAngle, maxAngle);
                targetZ = Mathf.Min(currentZ + stepAngle, maxAngle);
            } 
        }

        // 走るアニメーションのときだけ角度を変える
        if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) && !motionController.JumpFlag)
        {

            // 左移動
            // 0からー1
            // －1から－2
            if (changeCount == 0)
            {
                // ここで-１になる
                changeCount--;
                //currentZ = -45.0f;
                targetZ = -45.0f;
            }
            else if (changeCount == -1 && ray.IsWallRunLeft)
            {
                // -2になる
                changeCount--;
                //currentZ = -90.0f;
                targetZ = -90.0f;
            }
            // それ以外
            else if (changeCount > 0)
            {
                changeCount--;
                //currentZ = Mathf.Max(currentZ - stepAngle, -maxAngle);
                targetZ = Mathf.Max(currentZ - stepAngle, -maxAngle);
            }

        }

        // 目標のZ軸の角度に向かって現在のZ軸の角度を徐々に変える
        currentZ = Mathf.Lerp(currentZ, targetZ, Time.deltaTime * rotateSpeed);

        transform.rotation = Quaternion.Euler(0, 0, currentZ);


    }

    // 現在のZ軸の角度を取得するプロパティ
    public float CurrentZ { get { return currentZ; } }
}