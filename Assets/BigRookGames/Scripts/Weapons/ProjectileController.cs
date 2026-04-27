using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BigRookGames.Weapons
{
    public class ProjectileController : MonoBehaviour
    {
        //====== 設定 =====

        // 球の速度
        public float speed = 100;

        // 衝突判定のレイヤー
        public LayerMask collisionLayerMask;

        // 爆発エフェクト
        public GameObject rocketExplosion;

        // 球の見た目
        public MeshRenderer projectileMesh;

        // ====内部制御 ====

        // 何かに当たったかどうか
        private bool targetHit;

        // ==== サウンド ====

        // 飛行時の音
        public AudioSource inFlightAudioSource;

        // ==== VFX ====

        // ヒット時に止めるパーティクル
        public ParticleSystem disableOnHit;

        private void Start()
        {
            // 一定時間後に強制爆発
            Invoke("ExplodeIfNotHit", 1f); // 1秒後
        }

        private void Update()
        {
            // すでに当たっていたら動かさない
            if (targetHit) return;

            // 前方に移動
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
            foreach (Collider col in GetComponents<Collider>())
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
            // Destroy(gameObject);
        }
    }
}