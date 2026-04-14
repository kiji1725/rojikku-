using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerP : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rigid;
    private AudioSource audioSource;

    [Header("Movement")]
    [SerializeField] float jumpForce = 7.0f;
    [SerializeField] float laneMoveSpeed = 20.0f;
    [SerializeField] float forwardSpeed = 20.0f;
    [SerializeField] float speedIncreaseRate = 3.0f;
    [SerializeField] float speedUpDistance = 50f;
    [SerializeField] float distanceInterval = 50f;

    [Header("Jump Tuning")]
    [SerializeField] float fallMultiplier = 2.5f;   // 落下を速く
    [SerializeField] float lowJumpMultiplier = 2.0f;// 低ジャンプ調整

    [Header("Death Count")]
    [SerializeField] int maxDeathCount = 100;   // 何回死んだらゲームオーバー
    private static int deathCount = 0;        // 今回の死亡回数
    private static int totalDeathCount = 0;   // トータル死亡回数
    private static bool loaded = false;       // ★1回だけロード用


    [Header("Sound")]
    [SerializeField] AudioClip damageSE;
    [SerializeField] AudioClip jumpSE;

    private int currentLane = 1;
    private float startZ;
    private float[] positions = { -1.7f, 0f, 1.7f };

    private bool isGrounded = false;
    private bool dead = false;

    void Awake()
    {
        // ★最初の1回だけ保存データを読む
        if (!loaded)
        {
            totalDeathCount = PlayerPrefs.GetInt("TotalDeath", 0);
            loaded = true;
        }
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        rigid.freezeRotation = true;
        dead = false;

        Vector3 pos = transform.position;
        pos.x = positions[currentLane];
        transform.position = pos;

        startZ = pos.z;
    }



    void Update()
    {
        if (dead) return;

        // ジャンプ
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            animator.SetTrigger("JumpTrigger");
            rigid.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            if (jumpSE != null)
                audioSource.PlayOneShot(jumpSE);

            isGrounded = false;
        }

        // レーン移動
        if (Input.GetKeyDown(KeyCode.RightArrow))
            currentLane = Mathf.Min(currentLane + 1, 2);

        if (Input.GetKeyDown(KeyCode.LeftArrow))
            currentLane = Mathf.Max(currentLane - 1, 0);

        // 移動
        Vector3 pos = transform.position;
        pos.x = Mathf.Lerp(pos.x, positions[currentLane], Time.deltaTime * laneMoveSpeed);
        pos.z += forwardSpeed * Time.deltaTime;
        transform.position = pos;

        // スピードアップ
        float traveled = pos.z - startZ;
        if (traveled >= speedUpDistance)
        {
            forwardSpeed += speedIncreaseRate;
            speedUpDistance += distanceInterval;
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene("PauseScene");
            return;
        }
        // 落下ミス
        if (transform.position.y < -5f)
        {
            Miss();
        }
    }

    void FixedUpdate()
    {
        // 落下を速くしてジャンプを気持ちよく
        if (rigid.velocity.y < 0)
        {
            rigid.velocity += Vector3.up * Physics.gravity.y
                * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }
        // 低ジャンプ調整
        else if (rigid.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            rigid.velocity += Vector3.up * Physics.gravity.y
                * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // 着地判定（下方向のみ）
        if (collision.gameObject.CompareTag("Ground"))
        {
            foreach (ContactPoint contact in collision.contacts)
            {
                if (contact.normal.y > 0.5f)
                {
                    isGrounded = true;
                    animator.ResetTrigger("JumpTrigger");
                    break;
                }
            }
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Miss();
        }
    }

    // ======================
    // 被ダメ処理
    // ======================
    void Miss()
    {
        if (dead) return;

        dead = true;
        deathCount++;   // ← 死亡回数を増やす
        totalDeathCount++;   // ★トータル

        if (damageSE != null)
            audioSource.PlayOneShot(damageSE);

        Debug.Log("死亡回数：" + deathCount);
        Debug.Log("トータル死亡回数：" + totalDeathCount);
        if (deathCount >= maxDeathCount)
        {
            SceneManager.LoadScene("GameOver");
            return;
        }

        animator.SetBool("Fall", true);
        StartCoroutine(RestartStage());
    }

    IEnumerator RestartStage()
    {
        yield return new WaitForSeconds(2f);
        dead = false;   // ← 超重要
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public static int GetDeathCount()
    {
        return deathCount;
    }

    public static int GetTotalDeathCount()
    {
        return totalDeathCount;
    }

    // ====== 今回分だけリセット ======
    public static void ResetDeathCount()
    {
        deathCount = 0;
    }
}
