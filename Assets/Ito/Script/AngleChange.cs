using UnityEngine;

public class AngleChange : MonoBehaviour
{
    // 角度を変えるためのAnimatorとPlayerMotionControllerをインスペクターで設定
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerMotionController motionController;

    // string GR = "GunRun";

    // 角度の最大値とステップ値を定数として定義
    public const float maxAngle = 90f;
    public const float stepAngle = 45f;

    // 角度を変えるスピードをインスペクターで設定
    [SerializeField] private float rotateSpeed = 10f;

    // 目標のZ軸の角度と現在のZ軸の角度を保持する変数
    float targetZ = 0f;
    float currentZ = 0f;

    void Update()
    {
        // 走るアニメーションのときだけ角度を変える
        if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) && !motionController.JumpFlag)
        currentZ = Mathf.Min(currentZ + stepAngle, maxAngle);

        // 走るアニメーションのときだけ角度を変える
        if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) && !motionController.JumpFlag)
        currentZ = Mathf.Max(currentZ - stepAngle, -maxAngle);

        // 目標のZ軸の角度に向かって現在のZ軸の角度を徐々に変える
        currentZ = Mathf.Lerp(currentZ, targetZ, Time.deltaTime * rotateSpeed);

        transform.rotation = Quaternion.Euler(0, 0, currentZ);

        // 壁の問題が解決したらRaycastで判定して壁があるところだけ走れるようにする



    }

    // 現在のZ軸の角度を取得するプロパティ
    public float CurrentZ { get { return currentZ; } }
}