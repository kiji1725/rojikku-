using UnityEngine;
using TMPro;

public class ResultScore : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI resultText;

    void Start()
    {
        ShowScore();
    }

    // ▼スコア表示
    void ShowScore()
    {
        if (resultText != null)
        {
            int score = ScoreManager.instance.GetScore();

            resultText.text = "Score : " + score;
        }
    }
}