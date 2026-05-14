using UnityEngine;
using TMPro;
using System.Collections;

public class ResultScore : MonoBehaviour
{
    // テキスト
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI rankText;
    // カウントアップの時間
    [SerializeField] float duration = 2f;
    // 最終スコア
    int finalScore;

    // 最初に呼ばれる
    void Start()
    {
        // スコアの取得
        finalScore = ScoreManager.instance !=
            null ? ScoreManager.instance.GetScore() : 0;
        // カウントアップ開始
        StartCoroutine(ResultFlow());
    }

    // 結果表示の流れ
    IEnumerator ResultFlow()
    {
        // 最初は空にしておく
        scoreText.text = "";
        rankText.text = "";
        // スコアのカウントアップ
        float time = 0f;
        // カウントアップのループ
        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;
            int current = Mathf.FloorToInt(Mathf.Lerp(0, finalScore, t));
            scoreText.text = "Score : " + current;
            yield return null;
        }
        // 最終スコアを表示
        scoreText.text = "Score : " + finalScore;
        // 少し待つ
        yield return new WaitForSeconds(0.5f);
        // ランクの取得と表示
        string rank = GetRank(finalScore);
        rankText.text = "Rank : " + rank;
        rankText.alpha = 0;
        // ランクのフェードイン
        yield return StartCoroutine(FadeIn(rankText, 0.5f));
    }

    // ランクのフェードイン
    IEnumerator FadeIn(TextMeshProUGUI text, float duration)
    {
        //透明
        float time = 0f;
        // ループ
        while (time < duration)
        {
            time += Time.deltaTime;
            text.alpha = time / duration;
            yield return null;
        }
        // 表示
        text.alpha = 1;
    }

    // スコアに応じたランク
    string GetRank(int score)
    {
        if (score >= 325) return "SSS+";
        if (score >= 300) return "SSS";
        if (score >= 275) return "SS+";
        if (score >= 250) return "SS";
        if (score >= 225) return "S+";
        if (score >= 200) return "S";
        if (score >= 175) return "A+";
        if (score >= 150) return "A";
        if (score >= 125) return "B+";
        if (score >= 100) return "B";
        if (score >= 75) return "C+";
        if (score >= 50) return "C";
        if (score >= 25) return "D";
        return "E";
    }
}