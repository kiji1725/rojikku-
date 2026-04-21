using UnityEngine;

public class Angle_test : MonoBehaviour
{

    
    // ジャンプして矢印キーで壁走りできるようにしてみたい


    [SerializeField] private Animator animator;
    [SerializeField] private PlayerMotionController motionController;

    

    public const float maxAngle = 90f;
    public const float stepAngle = 45f;


    float currentZ = 0f;

    void Update()
    {

        if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) && !motionController.JumpFlag)
            currentZ = Mathf.Min(currentZ + stepAngle, maxAngle);

        if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) && !motionController.JumpFlag)
            currentZ = Mathf.Max(currentZ - stepAngle, -maxAngle);


        transform.rotation = Quaternion.Euler(0, 0, currentZ);

        // 壁の問題が解決したらRaycastで判定して壁があるところだけ走れるようにする



    }

    // プレイヤーに渡してどこを走っているか判定
    public float CurrentZ { get { return currentZ; } }

}
