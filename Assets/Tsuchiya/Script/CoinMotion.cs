using UnityEngine;

public class CoinMotion : MonoBehaviour
{
    // ▼回転速度
    public float rotateSpeed = 200f;

    // ▼上下移動の幅
    public float floatHeight = 0.5f;

    // ▼上下移動の速さ
    public float floatSpeed = 2f;

    private Vector3 startPos;

    void Start()
    {
        // 初期位置を保存
        startPos = transform.position;
    }

    void Update()
    {
        RotateCoin();
        FloatCoin();
    }

    // ▼コインを回転させる
    void RotateCoin()
    {
        transform.Rotate(0f, rotateSpeed * Time.deltaTime, 0f);
    }

    // ▼コインを上下にふわふわ動かす
    void FloatCoin()
    {
        float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = new Vector3(startPos.x, newY, startPos.z);
    }
}