using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeInSelection : MonoBehaviour
{
    public UnityEngine.UI.Image fadePanel;
    public float fadeDuration = 1f;

    void Start()
    {
        fadePanel.transform.SetAsLastSibling();
        StartCoroutine(FadeInCoroutine());
    }

    IEnumerator FadeInCoroutine()
    {
        float time = 0f;
        Color color = fadePanel.color;

        // ç≈èâÇÕê^Ç¡çïÇ…Ç∑ÇÈ
        color.a = 1f;
        fadePanel.color = color;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            color.a = 1f - (time / fadeDuration);
            fadePanel.color = color;
            yield return null;
        }

        color.a = 0f;
        fadePanel.color = color;

        fadePanel.transform.SetAsFirstSibling();
    }
}
