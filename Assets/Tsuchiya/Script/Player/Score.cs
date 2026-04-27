using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] Transform player;

    float realScore = 0;
    int displayScore = 0;

    [SerializeField] float scoreMultiplier = 1.5f;
    [SerializeField] float countSpeed = 200f;

    bool isStop = false;

    void Update()
    {
        if (player == null) return;

        if (!isStop)
        {
            float raw = player.position.z;
            realScore = raw * scoreMultiplier;

            if (ScoreManager.instance != null)
                ScoreManager.instance.SetScore(realScore);
        }

        UpdateDisplayScore();
        UpdateText();
    }

    void UpdateDisplayScore()
    {
        if (ScoreManager.instance == null) return;

        int target = ScoreManager.instance.GetScore();

        if (displayScore < target)
        {
            displayScore += Mathf.CeilToInt(countSpeed * Time.deltaTime);

            if (displayScore > target)
                displayScore = target;
        }
    }

    void UpdateText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score : " + displayScore;
        }
    }

    // ▼ゲーム終了時に呼ぶ
    public void StopScore()
    {
        isStop = true;
    }

    // ▼最終スコア取得（超重要）
    public float GetCurrentScore()
    {
        return realScore;
    }
}