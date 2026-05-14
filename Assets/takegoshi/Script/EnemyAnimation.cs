using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;

    void Update()
    {        
        animator.SetBool("Run", true);

    }

    public void RunOff()
    {

        animator.SetBool("Run", false);
    }


}
