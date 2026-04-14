using UnityEngine;

// 障害物を吹っ飛ばす処理を管理するクラス
public class ObstacleBlow : MonoBehaviour
{
    //使い方
    //BlowManagerに飛ばしたいオブジェクトを入れる。
    //発動条件を満たすと飛ぶ


    // ▼吹っ飛ばす強さ
    public float power = 10f;

    // ▼飛ぶ方向の調整
    public float forwardForce = 1f;   // 前方向（X軸想定）
    public float upwardForce = 0.3f;  // 上方向（Y軸）

    // ▼オブジェクト削除までの時間（秒）
    public float destroyDelay = 3f;

    // ▼吹っ飛ばす対象（Inspectorで設定）
    public GameObject[] targets;

    void Update()
    {
        // ▼発動条件
        // 現在：スペースキー入力で発動（デバッグ用）
        // 将来：プレイヤー接触時に発動する仕様に変更予定
        // TODO: プレイヤー接触で発動する処理を実装する
        if (Input.GetKeyDown(KeyCode.Space))
        {
            BlowObjects();
        }
    }

    // ▼対象オブジェクトを吹っ飛ばす処理
    void BlowObjects()
    {
        foreach (GameObject obj in targets)
        {
            // Rigidbodyがついているか確認
            Rigidbody rb = obj.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // ▼吹っ飛ばす方向ベクトルを作成
                // X方向（前）＋Y方向（上）に力を加える
                Vector3 force = new Vector3(forwardForce, upwardForce, 0f).normalized;

                // ▼力を加える
                rb.AddForce(force * power, ForceMode.Impulse);

                // ▼一定時間後に削除
                Destroy(obj, destroyDelay);
            }
            else
            {
                // Rigidbodyが無い場合は警告
                Debug.LogWarning(obj.name + " にRigidbodyがありません");
            }
        }
    }
}