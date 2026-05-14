using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Normal : MonoBehaviour
{
    // フェード
    public UnityEngine.UI.Image fadePanel;
    public float fadeDuration = 1f;
    // SE
    [Header("SE")]
    public AudioSource audioSource;
    public AudioClip startSE;
    // SE再生前の待機時間
    [Header("待機時間")]
    public float delayBeforeSE = 0.5f;

    // ボタンから呼ぶ
    public void StartFade()
    {
        // フェードパネルを最前面に移動
        fadePanel.transform.SetAsLastSibling();
        // SE再生とフェードを順番に実行
        StartCoroutine(PlaySEAndFade());
    }

    // SE再生とフェードを順番に実行するコルーチン
    IEnumerator PlaySEAndFade()
    {
        // SE再生前の待機
        yield return new WaitForSeconds(delayBeforeSE);
        // SE再生
        if (audioSource != null && startSE != null)
        {
            // SE再生
            audioSource.PlayOneShot(startSE);
            // SE終了待ち
            yield return new WaitForSeconds(startSE.length);
        }

        // フェード開始
        fadePanel.transform.SetAsLastSibling();
        // フェードアウト完了まで待機
        yield return StartCoroutine(FadeOut());
        // シーン切り替え
        SceneManager.LoadScene("stage2");
    }

    // フェードアウト処理
    IEnumerator FadeOut()
    {
        // フェードアウトの進行時間
        float time = 0f;
        // フェードパネルの色を取得
        Color color = fadePanel.color;
        // フェードアウト処理
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            time += Time.deltaTime;
            color.a = time / fadeDuration;
            fadePanel.color = color;
            yield return null;
        }
        // フェードアウト完了後、アルファ値を完全に1にする
        color.a = 1f;
        // フェードパネルの色を更新
        fadePanel.color = color;
    }
}
