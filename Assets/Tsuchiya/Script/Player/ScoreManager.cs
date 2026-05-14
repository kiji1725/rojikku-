using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    // ScoreManager本体
    public static ScoreManager instance;
    // スコア
    float score;

    void Awake()
    {
        // シングルトンの実装
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        // 既にScoreManagerが存在する場合は、破棄
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }

    // スコアを加算
    public void AddScore(float value)
    {
        score += value;
    }

    // スコアを減算
    public void SetScore(float newScore)
    {
        score = newScore;
    }
    
    // スコアを取得
    public int GetScore()
    {
        return Mathf.Max(0, Mathf.FloorToInt(score));
    }

    // スコアをリセット
    public void ResetScore()
    {
        score = 0;
    }
}