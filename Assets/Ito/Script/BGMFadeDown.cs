using System.Collections;
using UnityEngine;

public class BGMFadeDown : MonoBehaviour
{
    [Header("BGM")]
    public AudioSource bgmSource;

    [Header("‰¹—Êİ’è")]
    public float targetVolume = 0.2f; // ¬‚³‚­‚µ‚½Œã‚Ì‰¹—Ê
    public float fadeTime = 1f;       // ‰¹—Ê‚ª‰º‚ª‚éŠÔ

    // ƒ{ƒ^ƒ“‚©‚çŒÄ‚Ô
    public void FadeDownBGM()
    {
        StartCoroutine(FadeVolume());
    }

    IEnumerator FadeVolume()
    {
        float startVolume = bgmSource.volume;
        float time = 0f;

        while (time < fadeTime)
        {
            time += Time.deltaTime;

            bgmSource.volume = Mathf.Lerp(
                startVolume,
                targetVolume,
                time / fadeTime
            );

            yield return null;
        }

        bgmSource.volume = targetVolume;
    }
}