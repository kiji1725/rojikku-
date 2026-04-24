using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneMove2 : MonoBehaviour
{
    public UnityEngine.UI.Image fadePanel;
    public float fadeDuration = 1f;
    public string nextSceneName;

    // É{É^ÉìÇ©ÇÁåƒÇ‘
    public void StartFade()
    {
        fadePanel.transform.SetAsLastSibling(); // ç≈ëOñ Ç…
        StartCoroutine(FadeAndLoad());
    }

    IEnumerator FadeAndLoad()
    {
        yield return StartCoroutine(FadeOut());
        SceneManager.LoadScene("DifficultySelection");
    }

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
