using UnityEngine;
using UnityEngine.UI;
public class Pose : MonoBehaviour
{
    // ポーズ中か確認
    private bool isPaused = false;
    // インスペクターでポーズボタンを設定する
    public Button pauseButton;

    void Update()
    {
        // Escapeキーでポーズの切り替え
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }
    
    // ポーズの切り替え
    public void TogglePause()
    {
        // 停止中なら再開
        if (isPaused)
            ResumeGame();
        // 動いていたら停止
        else
            PauseGame();
    }

    // ゲームを停止する
    public void PauseGame()
    {
        // 時間を止める
        Time.timeScale = 0f;
        // ポーズにする
        isPaused = true;
        // ボタンの色を青くする
        SetButtonColor(new Color(0.4f, 0.8f, 1f));
    }

    // ゲームを再開する
    public void ResumeGame()
    {
        // 時間を戻す
        Time.timeScale = 1f;
        // ポーズ解除
        isPaused = false;
        // ボタンの色を白くする
        SetButtonColor(Color.white);
    }

    // ボタンの色を変更
    void SetButtonColor(Color color)
    {
        // 今のボタンの色を取得
        ColorBlock colors = pauseButton.colors;
        // 通常時の色に変更
        colors.normalColor = color;
        // 変更した色をボタンに適用
        pauseButton.colors = colors;
    }
}
