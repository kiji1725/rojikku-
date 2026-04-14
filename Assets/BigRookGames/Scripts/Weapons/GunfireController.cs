using UnityEngine;

namespace BigRookGames.Weapons
{
    public class GunfireController : MonoBehaviour
    {
        // --- Audio ---
        public AudioClip GunShotClip;       //発射音
        public AudioClip ReloadClip;        //リロード音
        public AudioSource source;          //発射音を鳴らすAudioSource
        public AudioSource reloadSource;    //リロード音を鳴らすAudioSource
        public Vector2 audioPitch = new Vector2(.9f, 1.1f); //音の高さをランダムに変えて単調さを防ぐ

        // --- Muzzle ---
        public GameObject muzzlePrefab;     //発射時のエフェクト
        public GameObject muzzlePosition;   //銃口の位置

        // --- Config ---
        public bool autoFire;           //自動で発射するか
        public float shotDelay = .5f;   //発射の間隔
        public bool rotate = true;      //銃を回転させるか(見た目)
        public float rotationSpeed = .25f;  //回転速度

        // --- Options ---
        public GameObject scope;          //スコープのオブジェクト
        public bool scopeActive = true;   //スコープON/OFF切り替え
        private bool lastScopeState;

        // --- Projectile ---
        [Tooltip("The projectile gameobject to instantiate each time the weapon is fired.")]
        public GameObject projectilePrefab;
        [Tooltip("Sometimes a mesh will want to be disabled on fire. For example: when a rocket is fired, we instantiate a new rocket, and disable" +
            " the visible rocket attached to the rocket launcher")]
        public GameObject projectileToDisableOnFire;

        // --- Timing ---
        [SerializeField] private float timeLastFired;   //最後に発射した時間


        private void Start()
        {
            if(source != null) source.clip = GunShotClip;
            timeLastFired = 0;
            lastScopeState = scopeActive;
        }

        private void Update()
        {
            // --- If rotate is set to true, rotate the weapon in scene ---
            if (rotate)     //武器を回転させる
            {
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y 
                                                                        + rotationSpeed, transform.localEulerAngles.z);
            }

            // --- Fires the weapon if the delay time period has passed since the last shot ---
            if (autoFire && ((timeLastFired + shotDelay) <= Time.time))     //一定時間ごとに FireWeapon() を実行
            {
                FireWeapon();
            }

            // --- Toggle scope based on public variable value ---
            if(scope && lastScopeState != scopeActive)
            {
                lastScopeState = scopeActive;
                scope.SetActive(scopeActive);       //スコープのON/OFF切り替え
            }
        }

        /// <summary>
        /// Creates an instance of the muzzle flash.
        /// Also creates an instance of the audioSource so that multiple shots are not overlapped on the same audio source.
        /// Insert projectile code in this function.
        /// </summary>
        public void FireWeapon()
        {
            // --- Keep track of when the weapon is being fired ---
            timeLastFired = Time.time;      // 発射時間更新

            // --- Spawn muzzle flash ---
            var flash = Instantiate(muzzlePrefab, muzzlePosition.transform);        //マズルフラッシュ生成

            // --- Shoot Projectile Object ---
            if (projectilePrefab != null)
            {
                GameObject newProjectile = Instantiate(projectilePrefab, 
                    muzzlePosition.transform.position, muzzlePosition.transform.rotation, transform);   //弾の生成
            }

            // --- Disable any gameobjects, if needed ---
            if (projectileToDisableOnFire != null)
            {
                projectileToDisableOnFire.SetActive(false);
                Invoke("ReEnableDisabledProjectile", 3);
                // 弾の非表示（ロケット用など）3秒後に再表示
            }

            // --- Handle Audio ---
            if (source != null)
            {
                // --- Sometimes the source is not attached to the weapon for easy instantiation on quick firing weapons like machineguns, 
                // so that each shot gets its own audio source, but sometimes it's fine to use just 1 source. We don't want to instantiate 
                // the parent gameobject or the program will get stuck in a loop, so we check to see if the source is a child object ---
                if(source.transform.IsChildOf(transform))
                {
                    source.Play();      //音の再生　パターン①：AudioSourceが子オブジェクト
                }
                else
                {
                    // --- Instantiate prefab for audio, delete after a few seconds ---
                    AudioSource newAS = Instantiate(source);        // パターン②：別オブジェクト（連射対応）
                    if (newAS != null &&
                        newAS.outputAudioMixerGroup != null && 
                        newAS.outputAudioMixerGroup.audioMixer != null)
                    {
                        // --- Change pitch to give variation to repeated shots ---
                        newAS.outputAudioMixerGroup.audioMixer.SetFloat("Pitch", Random.Range(audioPitch.x, audioPitch.y));
                        newAS.pitch = Random.Range(audioPitch.x, audioPitch.y);

                        // --- Play the gunshot sound ---
                        newAS.PlayOneShot(GunShotClip);

                        // --- Remove after a few seconds. Test script only. When using in project I recommend using an object pool ---
                        Destroy(newAS.gameObject, 4);
                    }
                }
            }

            // --- Insert custom code here to shoot projectile or hitscan from weapon ---

        }

        private void ReEnableDisabledProjectile()
        {
            reloadSource.Play();                        // リロード音の再生
            projectileToDisableOnFire.SetActive(true);  // 非表示にした弾の再表示
        }
    }
}