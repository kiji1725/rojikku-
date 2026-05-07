using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeOutTitle : MonoBehaviour
{
    public UnityEngine.UI.Image fadePanel;
    public float fadeDuration = 1f;

    [Header("SE")]
    public AudioSource audioSource;
    public AudioClip startSE;

    public string nextSceneName;

    // ボタンから呼ぶ
    public void StartFade()
    {
        fadePanel.transform.SetAsLastSibling(); // 最前面に
        StartCoroutine(PlaySEAndFade());
    }

    IEnumerator PlaySEAndFade()
    {
        // SE再生
        if (startSE != null && audioSource != null)
        {
            audioSource.PlayOneShot(startSE);

            // SEが鳴り終わるまで待機
            yield return new WaitForSeconds(startSE.length);
        }

        // フェード開始
        fadePanel.transform.SetAsLastSibling();

        yield return StartCoroutine(FadeOut());

        // シーン切り替え
        SceneManager.LoadScene("DifficultySelection");
    }


    /*
    IEnumerator FadeAndLoad()
    {
        yield return StartCoroutine(FadeOut());
        SceneManager.LoadScene("DifficultySelection");
    }
     */

    IEnumerator FadeOut()
    {
        float time = 0f;
        Color color = fadePanel.color;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;

            color.a = time / fadeDuration;
            fadePanel.color = color;

            yield return null;
        }

        color.a = 1f;
        fadePanel.color = color;
    }
}