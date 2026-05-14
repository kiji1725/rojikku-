using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Change : MonoBehaviour
{
    // シーン切り替えのためのマテリアルとスピードをインスペクターで設定
    [SerializeField] private Material dissolveMat;
    [SerializeField] private float speed = 1.5f;
    // シーン切り替え中かどうかを管理するフラグ
    bool isTransitioning = false;

    // シーン開始時にマテリアルのカットオフ値をリセット
    void Start()
    {
        // シーン開始時は見える状態に戻す
        dissolveMat.SetFloat("_Cutoff", 0f);
    }

    void Update()
    {
        // Enterキーでシーン切り替え
        if (!isTransitioning && Input.GetKeyDown(KeyCode.Return))
        {
            // シーン切り替えのコルーチンを開始
            StartCoroutine(DissolveAndChangeScene("2"));
        }
    }

    // シーン切り替えのコルーチン
    IEnumerator DissolveAndChangeScene(string sceneName)
    {
        // シーン切り替え中フラグを立てる
        isTransitioning = true;
        // カットオフ値を徐々に増やしていく
        float t = 0f;
        // tが1になるまでループしてカットオフ値を更新
        while (t < 1f)
        {
            t += Time.deltaTime * speed;
            dissolveMat.SetFloat("_Cutoff", t);
            yield return null;
        }
        // シーン切り替え
        SceneManager.LoadScene(sceneName);
    }
}
