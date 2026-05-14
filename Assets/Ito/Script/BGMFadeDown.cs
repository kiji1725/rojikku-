using System.Collections;
using UnityEngine;

public class BGMFadeDown : MonoBehaviour
{
    // BGMをフェードアウトさせるためにインスペクターで設定
    [Header("BGM")]
    public AudioSource bgmSource;
    // フェードアウトの設定
    [Header("音量設定")]
    public float targetVolume = 0.2f; // 小さくした後の音量
    public float fadeTime = 1f;       // 音量が下がる時間

    // ボタンから呼ぶ
    public void FadeDownBGM()
    {
        StartCoroutine(FadeVolume());
    }

    // 音量を徐々に下げるコルーチン
    IEnumerator FadeVolume()
    {
        // フェードアウトの開始時の音量を保存
        float startVolume = bgmSource.volume;
        float time = 0f;
        // フェードアウトの時間が経過するまでループ
        while (time < fadeTime)
        {
            time += Time.deltaTime;

            bgmSource.volume = Mathf.Lerp(startVolume,targetVolume,time / fadeTime);

            yield return null;
        }
        // フェードアウトが完了したら、音量を完全に小さくする
        bgmSource.volume = targetVolume;
    }
}