using UnityEngine;

public class PlayerMotionController : MonoBehaviour
{

    [SerializeField] private Animator PlayerAnimator;

    // アニメーション用文字列
    string ads = "ADS";
    string run = "Run";
    string slide = "Slide";
    string jump = "Jump";


    // ジャンプ用
    [SerializeField] float jumpForce = 5f;
    bool jumpFlag = false;
    // [SerializeField] private Rigidbody rb;




    void Start()
    {

    }

    
    void Update()
    {

        if (Input.GetMouseButtonDown(1))
        {

            //Debug.Log("on");
            PlayerAnimator.SetTrigger(ads);
        }
        if (Input.GetMouseButtonUp(1))
        {
            //Debug.Log("off");
            PlayerAnimator.SetTrigger(run);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            PlayerAnimator.SetTrigger(slide);
            
        }

        if (Input.GetKeyDown(KeyCode.W) 
            || Input.GetKeyDown(KeyCode.UpArrow)
            && !jumpFlag)
        {

            PlayerAnimator.SetTrigger(jump);

        }



    }

    // アニメーションのEventで使う用の関数
    public void IsJumpFragOn()
    {
        jumpFlag = true;
    }
    public void IsJumpFragOff()
    {
        jumpFlag = false;
    }


    public void SetRifleRun()
    {
        PlayerAnimator.SetTrigger(run);
    }

}
