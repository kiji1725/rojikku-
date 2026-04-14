using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("ƒS[ƒ‹");
        Debug.Log("1-2ƒNƒŠƒA");
        SceneManager.LoadScene("Clear");

    }
}
