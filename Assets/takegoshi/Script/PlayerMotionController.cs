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
    [SerializeField] private Rigidbody rb;
    private bool isGround;




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
            && isGround)
        {
            PlayerAnimator.SetTrigger(jump);

            Jump();
        }



    }


    public void Jump()
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


    public void SetRifleRun()
    {
        PlayerAnimator.SetTrigger(run);
    }

}
