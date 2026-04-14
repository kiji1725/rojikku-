using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform target;

    [SerializeField] Vector3 offset = new Vector3(0.0f, 2.0f, -4.0f);
    [SerializeField] Vector3 lookOffset = new Vector3(0, 1.5f, 2f);

    [Header("Zoom Settings")]
    [SerializeField] float zoomSpeed = 2.0f;
    [SerializeField] float minDistance = -2.0f;
    [SerializeField] float maxDistance = -8.0f;

    void LateUpdate()
    {
        if (target == null) return;


        // マウスホイール入力
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll != 0f)
        {
            offset.z += scroll * zoomSpeed;
            offset.z = Mathf.Clamp(offset.z, maxDistance, minDistance);
        }

        // カメラ位置
        Vector3 cameraPos = target.position + target.rotation * offset;
        transform.position = cameraPos;

        // 注視点
        Vector3 lookPos = target.position + target.rotation * lookOffset;
        transform.LookAt(lookPos);
    }
}