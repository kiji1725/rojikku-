using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnd : MonoBehaviour
{
    // スコア
    [SerializeField] Score score;
    // ゲームオーバーとゲームクリアで同じ処理をするため、共通の関数
    public void GameOver(){EndGame();}
    public void GameClear(){EndGame();}

    // ゲーム終了の処理
    void EndGame()
    {
        // スコアがない場合は処理しない
        if (score == null) return;
        // スコアの計算を止める
        score.StopScore();
        // スコアをスコアマネージャーに渡す
        if (ScoreManager.instance != null)
        {
            ScoreManager.instance.SetScore(score.GetCurrentScore());
        }
        // 結果シーンに遷移
        Invoke(nameof(LoadResult), 0.1f);
    }

    // 結果シーンの読み込み
    void LoadResult()
    {
        SceneManager.LoadScene("ResultScene");
    }
}