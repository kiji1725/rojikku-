using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            GOAL();


        }
    }
    public void GOAL()
    {
      
        SceneManager.LoadScene("GameClear");

    }
}
