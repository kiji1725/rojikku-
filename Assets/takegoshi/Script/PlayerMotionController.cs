using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

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
    [SerializeField] float playerGravity = -15.0f;
    bool jumpFlag = false;
    bool isGround = false;

    public bool isSliding = false;
    // bool isADS = false;

    [SerializeField] private AngleChange angleChange;

    void Start()
    {

    }

    private void FixedUpdate()
    {
        // 毎フレーム重力をかける
        if (!isGround)
        {
            rb.linearVelocity += Vector3.up * playerGravity * Time.fixedDeltaTime;
        }
    }


    void Update()
    {
        // ADS右クリック長押し あんま使わない
        if (Input.GetMouseButtonDown(1))
        {
            PlayerAnimator.SetTrigger(ads);
        }
        if (Input.GetMouseButtonUp(1))
        {
            PlayerAnimator.SetTrigger(run);
        }

        // スライディング↓orSキー
        if (  ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && !jumpFlag && !isSliding) && angleChange.CurrentZ == 0 )
        {
            isSliding = true;
            PlayerAnimator.SetTrigger(slide);
        }
        // ジャンプ↑orWキー
        if ( ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && !jumpFlag && !isSliding) && angleChange.CurrentZ != 90 && angleChange.CurrentZ != -90)
        {

            jumpFlag = true;
            PlayerAnimator.SetTrigger(jump);
            Jump();

        }

    }
    
    // ジャンプ
    void Jump()
    {
        // 上方向の力
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        // プレイヤーの重力
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, playerGravity, rb.linearVelocity.z);
        isGround = false;
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;
        }
        
        // 落ちる床
        if (collision.gameObject.CompareTag("FF"))
        {
            isGround = false;
        }

        if (collision.gameObject.CompareTag("out"))
        {
            SceneManager.LoadScene("GameOver 1");
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        // アングルが90のときは即時リターン
        if (angleChange.CurrentZ == 90) return;
        // グランドから離れたら重力を加算
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = false;
        }
    }


    public bool IsSliding {  get { return isSliding; } }
    // ジャンプ中に左右移動させないため
    public bool JumpFlag { get { return jumpFlag; } }


    // アニメーションのEventに使う関数
    public void SetRifleRun()
    {
        PlayerAnimator.SetTrigger(run);
    }
    public void SlidingOff()
    {
        isSliding = false;
    }
    public void IsJump()
    {
        jumpFlag = false;
    }
    //public void IsADSOn()
    //{
    //    isADS = true;
    //}




}
