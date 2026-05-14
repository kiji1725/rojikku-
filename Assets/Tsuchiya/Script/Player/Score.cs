using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    // テキスト
    [SerializeField] TextMeshProUGUI scoreText;
    // プレイヤー
    [SerializeField] Transform player;
    // 実際のスコア
    float realScore = 0;
    int displayScore = 0;
    // スコア倍率
    [SerializeField] float scoreMultiplier = 1.5f;
    [SerializeField] float countSpeed = 200f;
    // スコアの更新を停止するか
    bool isStop = false;

    void Update()
    {
        // プレイヤーが存在しない際はスコアを更新しない
        if (player == null) return;
        // スコアの更新が停止されていない場合、スコアを更新する
        if (!isStop)
        {
            float raw = player.position.z;
            realScore = raw * scoreMultiplier;
            if (ScoreManager.instance != null)
                ScoreManager.instance.SetScore(realScore);
        }
        // 表示スコアを実際のスコアに近づける
        UpdateDisplayScore();
        UpdateText();
    }

    // 表示スコアを実際のスコアに近づける
    void UpdateDisplayScore()
    {
        // ScoreManagerが存在しない場合はスコアを更新しない
        if (ScoreManager.instance == null) return;
        // ScoreManagerから実際のスコアを取得
        int target = ScoreManager.instance.GetScore();
        // 表示スコアが実際のスコアより小さい場合、表示スコアを増加させる
        if (displayScore < target)
        {
            displayScore += Mathf.CeilToInt(countSpeed * Time.deltaTime);
            if (displayScore > target)displayScore = target;
        }
    }

    // スコアテキストを更新する
    void UpdateText()
    {
        // スコアテキストが存在する場合、スコアテキストを更新する
        if (scoreText != null)
        {
            scoreText.text = "Score : " + displayScore;
        }
    }
    
    // スコアの更新を停止する
    public void StopScore()
    {
        isStop = true;
    }

    // 現在のスコアを取得する
    public float GetCurrentScore()
    {
        return realScore;
    }
}