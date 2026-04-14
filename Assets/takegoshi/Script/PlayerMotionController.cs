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
    [SerializeField] private Rigidbody rb;
    [SerializeField] float jumpForce = 5f;
    public bool jumpFlag = false;
    public bool isGround = true;


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
        // スライディング↓orSキー
        if (Input.GetKeyDown(KeyCode.S)
            || Input.GetKeyDown(KeyCode.DownArrow))
        {
            PlayerAnimator.SetTrigger(slide);
            
        }
        // スライディング↑orWキー
        if ((Input.GetKeyDown(KeyCode.W) 
            || Input.GetKeyDown(KeyCode.UpArrow))
            && (!jumpFlag))
        {
            jumpFlag = true;
            PlayerAnimator.SetTrigger(jump);
            Jump();

        }



    }

    // ジャンプ
    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGround = false;
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;
        }
    }

    // アニメーションのEventに使う関数
    public void SetRifleRun()
    {
        PlayerAnimator.SetTrigger(run);
    }
    public void IsJump()
    {
        jumpFlag = false;
    }


}
