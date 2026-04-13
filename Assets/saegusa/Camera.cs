using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform player;                
    public Vector3 offset = new Vector3(0f, 60f, -30f);  
    public Vector3 rotation = new Vector3(50f, 0f, 0f);

    private float fixedX; // 初期位置基準のX座標

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (player == null)
        {
            Debug.LogError("プレイヤーが指定されていません！");
            return;
        }

        // プレイヤーの初期位置を基準にX座標を決定
        fixedX = player.position.x + offset.x;

        // カメラ初期位置設定
        transform.position = new Vector3(fixedX, player.position.y + offset.y, player.position.z + offset.z);
        transform.LookAt(player);
    }

    void Update()
    {
        if (player == null) return;

        // Xは最初に固定、Y,Zはプレイヤーをおうようにする
        transform.position = new Vector3(
            fixedX,
            player.position.y + offset.y,
            player.position.z + offset.z
        );

        transform.LookAt(player.position + Vector3.up * 20f);
    }
}
