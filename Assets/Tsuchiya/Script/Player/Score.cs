using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;

    // ▼内部スコア（実際の値）
    float realScore = 0;

    // ▼表示用スコア（1ずつ増える）
    int displayScore = 0;

    // ▼増え方の調整（ここを好きに触る）
    [SerializeField] float scoreMultiplier = 1.5f;

    // ▼UIが追いつく速さ
    [SerializeField] float countSpeed = 200f;

    void Update()
    {
        UpdateRealScore();
        UpdateDisplayScore();
        UpdateText();
    }

    // ▼距離 → スコア変換（ここを自由に調整）
    void UpdateRealScore()
    {
        float raw = transform.position.z;
        realScore = raw * scoreMultiplier;

        // Managerに保存
        ScoreManager.instance.SetScore(realScore);
    }

    // ▼1ずつ増えるように補間
    void UpdateDisplayScore()
    {
        int target = Mathf.FloorToInt(realScore);

        if (displayScore < target)
        {
            displayScore += Mathf.CeilToInt(countSpeed * Time.deltaTime);

            // 行き過ぎ防止
            if (displayScore > target)
                displayScore = target;
        }
    }

    // ▼UI表示
    void UpdateText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score : " + displayScore;
        }
    }
}