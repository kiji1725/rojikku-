using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnd : MonoBehaviour
{
    [SerializeField] Score score;

    public void GameOver()
    {
        EndGame();
    }

    public void GameClear()
    {
        EndGame();
    }

    void EndGame()
    {
        if (score == null) return;

        // ▼① スコア更新停止
        score.StopScore();

        // ▼② 最終スコアを強制保存（超重要）
        if (ScoreManager.instance != null)
        {
            ScoreManager.instance.SetScore(score.GetCurrentScore());
        }

        // ▼③ 少し待ってから遷移（これがないとズレる）
        Invoke(nameof(LoadResult), 0.1f);
    }

    void LoadResult()
    {
        SceneManager.LoadScene("ResultScene");
    }
}