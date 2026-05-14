using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeInSelection : MonoBehaviour
{
    // フェードインのUIと時間をインスペクターで設定
    public UnityEngine.UI.Image fadePanel;
    public float fadeDuration = 1f;

    // シーン開始時にフェードインを開始
    void Start()
    {
        // フェードパネルを最前面にしてからフェードインを開始
        fadePanel.transform.SetAsLastSibling();
        StartCoroutine(FadeInCoroutine());
    }

    // フェードインのコルーチン
    IEnumerator FadeInCoroutine()
    {
        // フェードインの時間の変数と、フェードパネルの色を取得
        float time = 0f;
        Color color = fadePanel.color;
        // 最初は黒
        color.a = 1f;
        fadePanel.color = color;
        // フェードインの時間が経過するまでループ
        while (time < fadeDuration)
        {
            // 時間を更新して、フェードパネルのα値を徐々に減らす
            time += Time.deltaTime;
            // α値は1から0に向かって減少するように計算
            color.a = 1f - (time / fadeDuration);
            // フェードパネルの色を更新
            fadePanel.color = color;
            // 次のフレームまで待つ
            yield return null;
        }
        // フェードインが完了したら、α値を完全に0にしてフェードパネルを非表示にする
        color.a = 0f;
        // フェードパネルの色を更新
        fadePanel.color = color;
        // フェードパネルを最前面から移動して、他のUI要素が表示されるようにする
        fadePanel.transform.SetAsFirstSibling();
    }
}
