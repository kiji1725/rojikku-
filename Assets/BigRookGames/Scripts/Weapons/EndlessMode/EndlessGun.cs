using UnityEngine;

public class EndlessGun : MonoBehaviour
{
    // --- 弾数管理 ---
    public int currentAmmo = 1;// 現在の弾数を1に設定
    public int maxAmmo = 3;// 弾数の最大値を3に設定

    // --- 繰り返す処理の管理 ---
    private float _repeatSpan;    //繰り返す間隔
    private float _timeElapsed;   //経過時間

    // --- 時間の初期化 ---
    private void Start()
    {
        currentAmmo = maxAmmo;
        _repeatSpan = 10;    //実行間隔を10に設定
        _timeElapsed = 0;   //経過時間をリセット
    }

    // --- 繰り返す処理の実行 ---
    private void Update()
    {
        _timeElapsed += Time.deltaTime;     //時間をカウントする

        //経過時間が繰り返す間隔を経過したら
        if (_timeElapsed >= _repeatSpan)
        {
            //ここで処理を実行
            AddAmmo(1);// 弾を増やす処理を呼び出す

            _timeElapsed -= _repeatSpan;   //経過時間をリセットする
        }
    }

    // --- 弾を増やす ---
    public void AddAmmo(int amount)// 弾を増やすときに弾数を増やす
    {
        currentAmmo += amount;// 弾数を増やす

        if (currentAmmo > maxAmmo)currentAmmo = maxAmmo;// 弾数が最大を超えないようにする

        Debug.Log("現在の弾数: " + currentAmmo);// 現在の弾数をログに表示
    }

    // --- 弾を使う ---
    public bool UseAmmo(int amount)// 弾を使うときに弾数を減らす
    {
        if (currentAmmo >= amount)// 弾が足りる場合
        {
            currentAmmo -= amount;// 弾を消費した後、弾数を減らす
            return true;// 弾が足りる場合はtrueを返す
        }
        Debug.Log("弾が足りません");
        return false;// 弾が足りない場合はfalseを返す
    }
    /*
    // --- 10秒ごとに球の数が回復する処理 ---
    private void OnTriggerEnter(Collider other)// 弾のオブジェクトに触れたときの処理
    {
        if (other.CompareTag("Ammo"))// 弾のタグを持つオブジェクトに触れたとき
        {
             // 弾を拾うときに弾数を増やす
            Destroy(other.gameObject);// 弾を拾った後、弾のオブジェクトを破壊
        }
    }
     */
}
