using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public float score;

    void Awake()
    {
        // ▼1つだけ残す
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // ←シーン跨ぎ
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // ▼スコア更新
    public void SetScore(float newScore)
    {
        score = newScore;
    }

    // ▼スコア取得
    public int GetScore()
    {
        return Mathf.FloorToInt(score);
    }

    // ▼リセット（リトライ用）
    public void ResetScore()
    {
        score = 0;
    }
}