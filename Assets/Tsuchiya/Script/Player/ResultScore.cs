using UnityEngine;
using TMPro;
using System.Collections;

public class ResultScore : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI titleText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI rankText;

    [SerializeField] float duration = 2f;

    int finalScore;

    void Start()
    {
        finalScore = ScoreManager.instance != null
            ? ScoreManager.instance.GetScore()
            : 0;

        StartCoroutine(ResultFlow());
    }

    IEnumerator ResultFlow()
    {
        titleText.alpha = 0;
        scoreText.text = "";
        rankText.text = "";

        // ▼タイトル（必要なら "CLEAR!!" に変更可）
        titleText.text = "GAME OVER";
        yield return StartCoroutine(FadeIn(titleText, 0.8f));

        yield return new WaitForSeconds(0.5f);

        // ▼スコアカウントアップ
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;

            int current = Mathf.FloorToInt(Mathf.Lerp(0, finalScore, t));
            scoreText.text = "Score : " + current;

            yield return null;
        }

        scoreText.text = "Score : " + finalScore;

        yield return new WaitForSeconds(0.5f);

        // ▼ランク
        string rank = GetRank(finalScore);
        rankText.text = "Rank : " + rank;
        rankText.alpha = 0;

        yield return StartCoroutine(FadeIn(rankText, 0.5f));
    }

    IEnumerator FadeIn(TextMeshProUGUI text, float duration)
    {
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            text.alpha = time / duration;
            yield return null;
        }

        text.alpha = 1;
    }

    string GetRank(int score)
    {
        if (score >= 3000) return "SSS+";
        if (score >= 2600) return "SSS";
        if (score >= 2300) return "SS+";
        if (score >= 2000) return "SS";
        if (score >= 1700) return "S+";
        if (score >= 1400) return "S";
        if (score >= 1200) return "A+";
        if (score >= 1000) return "A";
        if (score >= 800) return "B+";
        if (score >= 600) return "B";
        if (score >= 500) return "C+";
        if (score >= 400) return "C";
        if (score >= 300) return "D";
        return "E";
    }
}