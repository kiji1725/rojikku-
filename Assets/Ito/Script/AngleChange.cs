using UnityEngine;

public class AngleChange : MonoBehaviour
{
    // 角度を変えるためのAnimatorとPlayerMotionControllerをインスペクターで設定
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerMotionController motionController;
    // 壁走りの判定をするためのRayRightとRayLeftをインスペクターで設定
    [SerializeField] private RayRight rayRight;
    [SerializeField] private RayLeft rayLeft;

    // 角度を変えるための設定
    public float maxAngle = 90f;
    public float stepAngle = 45f;
    public bool isJust;
    public int changeCount = 0;

    // 角度を変えるスピードをインスペクターで設定
    [SerializeField] private float rotateSpeed = 50.0f;

    // 目標のZ軸の角度と現在のZ軸の角度を保持する変数
    float targetZ = 0f;
    [SerializeField] float currentZ = 0f;

    // SE
    [Header("SE")]
    public AudioSource audioSource;
    public AudioClip GravitySE;

    // 角度を変えるための処理
    private void FixedUpdate()
    {
        // ぴったりになるまで動かないようにする
        isJust =
            Mathf.Abs(Mathf.DeltaAngle(currentZ, 0.0f)) < 5.0f ||
            Mathf.Abs(Mathf.DeltaAngle(currentZ, -45.0f)) < 5.0f ||
            Mathf.Abs(Mathf.DeltaAngle(currentZ, +45.0f)) < 5.0f ||
            Mathf.Abs(Mathf.DeltaAngle(currentZ, -90.0f)) < 5.0f ||
            Mathf.Abs(Mathf.DeltaAngle(currentZ, +90.0f)) < 5.0f;
    }

    // 角度を変えるための処理
    void Update()
    {
        // 走るアニメーションのときだけ角度を変える
        if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) && !motionController.JumpFlag)
        {
            // 右移動、0~1、1~2
            if (changeCount == 0 && isJust)
            {
                // ここで１になる
                changeCount++;
                targetZ = 45.0f;
            }
            // 壁走り
            else if (changeCount == 1 && rayRight.IsWallRunRight && isJust)
            {
                // 2になる
                changeCount++;
                targetZ = 90.0f;
                audioSource.PlayOneShot(GravitySE);
            }
            // それ以外
            else if (changeCount < 0 && isJust)
            {
                changeCount++;
                targetZ = Mathf.Min(currentZ + stepAngle, maxAngle);
            } 
        }

        // ジャンプのときに角度を変えることができないようにする
        if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) && !motionController.JumpFlag)
        {
            // 左移動、0~-1、-1~-2
            if (changeCount == 0 && isJust)
            {
                // ここで-１になる
                changeCount--;
                targetZ = -45.0f;
            }
            // 壁走り
            else if (changeCount == -1 && rayLeft.IsWallRunLeft && isJust)
            {
                // -2になる
                changeCount--;
                targetZ = -90.0f;
                audioSource.PlayOneShot(GravitySE);

            }
            // それ以外
            else if (changeCount > 0 && isJust)
            {
                changeCount--;
                targetZ = Mathf.Max(currentZ - stepAngle, -maxAngle);
            }
        }
        // 現在のZ軸の角度を目標のZ軸の角度に近づける
        currentZ = Mathf.Lerp(currentZ, targetZ, Time.deltaTime * rotateSpeed);
        transform.rotation = Quaternion.Euler(0, 0, currentZ);
    }

    // 現在のZ軸の角度を取得するプロパティ
    public float CurrentZ { get { return currentZ; } }
}