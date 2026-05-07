using UnityEngine;

namespace BigRookGames.Weapons
{
    public class Endless: MonoBehaviour
    {
        // 音
        public AudioClip GunShotClip;       //発射音
        public AudioClip ReloadClip;        //リロード音
        public AudioSource source;          //発射音を鳴らすAudioSource
        public AudioSource reloadSource;    //リロード音を鳴らすAudioSource
        public Vector2 audioPitch = new Vector2(.9f, 1.1f); //音の高さをランダムに変えて単調さを防ぐ

        // 口輪
        public GameObject muzzlePrefab;     //発射時のエフェクト
        public GameObject muzzlePosition;   //銃口の位置

        // 設定
        public float shotDelay = .5f;   //発射の間隔
        public bool rotate = true;      //銃を回転させるか(見た目)
        public float rotationSpeed = .25f;  //回転速度

        // オプション
        public GameObject scope;          //スコープのオブジェクト
        public bool scopeActive = true;   //スコープON/OFF切り替え
        private bool lastScopeState;

        // プロジェクション
        [Tooltip("The projectile gameobject to instantiate each time the weapon is fired.")]
        public GameObject projectilePrefab;
        [Tooltip("Sometimes a mesh will want to be disabled on fire. For example: when a rocket is fired, we instantiate a new rocket, and disable" +
            " the visible rocket attached to the rocket launcher")]
        public GameObject projectileToDisableOnFire;

        // タイミング
        [SerializeField] private float timeLastFired;   //最後に発射した時間

        // 弾管理
        private EndlessGun playerAmmo;

        private void Start()
        {
            if (source != null) source.clip = GunShotClip;
            timeLastFired = 0;
            lastScopeState = scopeActive;

            playerAmmo = GetComponentInParent<EndlessGun>();
        }

        private void Update()
        {
            // シーン内で武器を回転させる
            if (rotate)     //武器を回転させる
            {
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y
                                                                        + rotationSpeed, transform.localEulerAngles.z);
            }

            // 前回の発射から遅延時間が経過していれば、武器を発射する
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (playerAmmo != null && playerAmmo.UseAmmo(1))// 弾を消費できる場合は発射
                {
                    FireWeapon();
                }
                else
                {
                    Debug.Log("弾がない");
                }
            }

            // スコープを切り替える
            if (scope && lastScopeState != scopeActive)
            {
                lastScopeState = scopeActive;
                scope.SetActive(scopeActive);       //スコープのON/OFF切り替え
            }
        }

        /// <summary>
        /// マズルフラッシュのインスタンスを作成
        /// 複数の発射音が同じオーディオソースで重ならないように、audioSource のインスタンスも作成します。
        /// この関数内に弾丸のコードを挿入してください。
        /// </summary>
        public void FireWeapon()
        {
            // 武器が発射されているときに記録をつける
            timeLastFired = Time.time;      // 発射時間更新

            // マズルフラッシュを生成
            var flash = Instantiate(muzzlePrefab, muzzlePosition.transform);        //マズルフラッシュ生成

            // 投射物オブジェクトを撃つ
            if (projectilePrefab != null)
            {
                GameObject newProjectile = Instantiate(projectilePrefab,
                    muzzlePosition.transform.position, muzzlePosition.transform.rotation, transform);   //弾の生成
            }

            // 必要に応じて、ゲームオブジェクトを無効にする
            if (projectileToDisableOnFire != null)
            {
                projectileToDisableOnFire.SetActive(false);
                Invoke("ReEnableDisabledProjectile", 3);
                // 弾の非表示（ロケット用など）3秒後に再表示
            }

            // オーディオを操作する
            if (source != null)
            {
                // 場合によっては、マシンガンのような連射武器での簡単なインスタンス化のために、
                // オーディオソースが武器に取り付けられていないことがあります。
                // これにより、各ショットが独自のオーディオソースを持つことができますが、時には1つのソースだけを使っても問題ありません。
                // 親のゲームオブジェクトをインスタンス化するとプログラムがループに陥る可能性があるため、ソースが子オブジェクトかどうかを確認します
                if (source.transform.IsChildOf(transform))
                {
                    source.Play();      //音の再生　パターン①：AudioSourceが子オブジェクト
                }
                else
                {
                    // オーディオ用のプレハブをインスタンス化し、数秒後に削除する
                    AudioSource newAS = Instantiate(source);        // パターン②：別オブジェクト（連射対応）
                    if (newAS != null &&
                        newAS.outputAudioMixerGroup != null &&
                        newAS.outputAudioMixerGroup.audioMixer != null)
                    {
                        // 繰り返されるショットに変化をつけるためにピッチを変える
                        newAS.outputAudioMixerGroup.audioMixer.SetFloat("Pitch", Random.Range(audioPitch.x, audioPitch.y));
                        newAS.pitch = Random.Range(audioPitch.x, audioPitch.y);

                        // 銃声の音を再生する
                        newAS.PlayOneShot(GunShotClip);

                        // 数秒後に削除してください。テストスクリプトのみです。プロジェクトで使用する場合は、オブジェクトプールの使用を推奨します
                        Destroy(newAS.gameObject, 4);
                    }
                }
            }

            // ここにカスタムコードを挿入して、武器から弾丸またはヒットスキャンを発射します

        }

        private void ReEnableDisabledProjectile()
        {
            reloadSource.Play();                        // リロード音の再生
            projectileToDisableOnFire.SetActive(true);  // 非表示にした弾の再表示
        }
    }
}
