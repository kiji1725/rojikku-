using System.Collections;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Normal : MonoBehaviour
{
    public UnityEngine.UI.Image fadePanel;
    public float fadeDuration = 1f;

    [Header("SE")]
    public AudioSource audioSource;
    public AudioClip startSE;


    // ボタンから呼ぶ
    public void StartFade()
    {
        fadePanel.transform.SetAsLastSibling(); // 最前面に
        StartCoroutine(PlaySEAndFade());
    }

    IEnumerator PlaySEAndFade()
    {
        // SE再生
        if (audioSource != null && startSE != null)
        {
            audioSource.PlayOneShot(startSE);

            // SE終了待ち
            yield return new WaitForSeconds(startSE.length);
        }

        // フェード開始
        fadePanel.transform.SetAsLastSibling();

        yield return StartCoroutine(FadeOut());

        // シーン切り替え
        SceneManager.LoadScene("stege1");
    }

    /*
    IEnumerator FadeAndLoad()
    {
        yield return StartCoroutine(FadeOut());
        SceneManager.LoadScene("Stege1");
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
