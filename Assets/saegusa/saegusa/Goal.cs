using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("ÉSÅ[Éã");
        SceneManager.LoadScene("Clear 1");

    }
}
