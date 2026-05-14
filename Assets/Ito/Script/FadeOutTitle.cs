using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeOutTitle : MonoBehaviour
{
    // フェード
    public UnityEngine.UI.Image fadePanel;
    public float fadeDuration = 1f;
    // SE
    [Header("SE")]
    public AudioSource audioSource;
    public AudioClip startSE;

    // ボタンから呼ぶ
    public void StartFade()
    {
        // フェードパネルを最前面に移動
        fadePanel.transform.SetAsLastSibling(); // 最前面に
        StartCoroutine(PlaySEAndFade());
    }

    // SEを再生してからフェードアウトするコルーチン
    IEnumerator PlaySEAndFade()
    {
        // SE再生
        if (startSE != null && audioSource != null)
        {
            // SEを再生
            audioSource.PlayOneShot(startSE);
            // SEが鳴り終わるまで待機
            yield return new WaitForSeconds(startSE.length);
        }
        // フェード開始
        fadePanel.transform.SetAsLastSibling();
        // フェードアウト
        yield return StartCoroutine(FadeOut());
        // シーン切り替え
        SceneManager.LoadScene("DifficultySelection");
    }

    // フェードアウトのコルーチン
    IEnumerator FadeOut()
    {
        // フェードアウトの開始
        float time = 0f;
        // 最初は透明にする
        Color color = fadePanel.color;
        // フェードアウトしていく
        while (time < fadeDuration)
        {
            // 時間を加算
            time += Time.deltaTime;
            // アルファ値を更新
            color.a = time / fadeDuration;
            // フェードパネルの色を更新
            fadePanel.color = color;
            // 次のフレームまで待機
            yield return null;
        }
        // フェードアウト完了後は完全に黒くする
        color.a = 1f;
        // フェードパネルの色を更新
        fadePanel.color = color;
    }
}