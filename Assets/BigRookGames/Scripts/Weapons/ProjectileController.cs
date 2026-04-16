using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BigRookGames.Weapons
{
    public class ProjectileController : MonoBehaviour
    {
        // 設定
        public float speed = 100;
        public LayerMask collisionLayerMask;

        // 爆発VFX
        public GameObject rocketExplosion;

        // 発射体メッシュ
        public MeshRenderer projectileMesh;

        // スクリプト変数
        private bool targetHit;

        // 音
        public AudioSource inFlightAudioSource;

        // VFX
        public ParticleSystem disableOnHit;

        private void Start()
        {
            Invoke("ExplodeIfNotHit", 3f); // 3秒後
        }

        private void Update()
        {
            // ターゲットがヒットしたかどうかを確認してください。ターゲットがヒットした場合は位置を更新したくありません
            if (targetHit) return;

            // ゲームオブジェクトを定義された速度で前方に移動させる
            transform.position += transform.forward * (speed * Time.deltaTime);

        }


        /// <summary>
        /// 接触すると爆発する。
        /// </summary>
        /// <param name="collision"></param>
        private void OnCollisionEnter(Collision collision)
        {
            // コンポーネントが無効でも OnCollision は呼ばれるので、有効でない場合はリターンします
            if (!enabled) return;

            // 物体に当たったときに爆発し、弾丸のメッシュを無効にする
            Explode();
            projectileMesh.enabled = false;
            targetHit = true;
            inFlightAudioSource.Stop();
            foreach(Collider col in GetComponents<Collider>())
            {
                col.enabled = false;
            }
            disableOnHit.Stop();
        }


        /// <summary>
        /// 爆発オブジェクトをインスタンス化します。
        /// </summary>
        private void Explode()
        {
            GameObject newExplosion =
                Instantiate
                (rocketExplosion, transform.position,
                rocketExplosion.transform.rotation, null);

            // エフェクトの長さを取得して、その後に消す
            ParticleSystem ps = newExplosion.GetComponent<ParticleSystem>();
            float duration = ps.main.duration;
            float startLifetime = ps.main.startLifetime.constantMax;

            Destroy(gameObject, duration + startLifetime);
        }
        private void ExplodeIfNotHit()
        {
            if (targetHit) return; // すでに当たってたら何もしない

            Explode();
            Destroy(gameObject);
        }
    }
}