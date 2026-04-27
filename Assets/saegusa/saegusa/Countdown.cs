using TMPro;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    public TextMeshProUGUI countdownText;
    public float timeLeft = 3f;

    void Update()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            countdownText.text = Mathf.Ceil(timeLeft).ToString();
        }
        else
        {
            countdownText.gameObject.SetActive(false);
        }
    }
}
