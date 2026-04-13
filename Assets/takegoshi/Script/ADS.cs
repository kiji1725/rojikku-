using UnityEngine;

public class ADS : MonoBehaviour
{

    [SerializeField] private Animator PlayerAnimator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(1))
        {

            //Debug.Log("on");
            PlayerAnimator.SetTrigger("ADS");
        }
        if (Input.GetMouseButtonUp(1))
        {
            //Debug.Log("off");
            PlayerAnimator.SetTrigger("RifleRun");
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            PlayerAnimator.SetTrigger("Slide");
            PlayerAnimator.SetTrigger("RifleRun");
        }
        


    }


    public void SetRifleRun()
    {
        PlayerAnimator.SetTrigger("RifleRun");
    }


}
