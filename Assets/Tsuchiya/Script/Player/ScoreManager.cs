using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    float score;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }

    public void AddScore(float value)
    {
        score += value;
    }

    public void SetScore(float newScore)
    {
        score = newScore;
    }

    public int GetScore()
    {
        return Mathf.Max(0, Mathf.FloorToInt(score));
    }

    public void ResetScore()
    {
        score = 0;
    }
}