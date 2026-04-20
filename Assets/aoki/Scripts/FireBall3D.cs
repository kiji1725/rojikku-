using UnityEngine;

public class FireBall3D : MonoBehaviour
{

    public float scaleSpeed = 5f;
    public float scaleAmount = 0.1f;
    public Transform targetCamera;

    // Update is called once per frame
    void Update()
    {
        // カメラの向きになる
        if (targetCamera == null) return;
        transform.forward = targetCamera.transform.forward;

        // アニメーション
        float scale = 1 + Mathf.Sin(Time.time * scaleSpeed) * scaleAmount;
        transform.localScale =new Vector3(scale, scale, scale);

        // 回転
        transform.Rotate(0, 0, 200 *  Time.deltaTime);
    }
}
