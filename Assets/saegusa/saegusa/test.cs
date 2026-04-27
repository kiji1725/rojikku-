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
    [SerializeField] float fallMultiplier = 2.5f;   // �����𑬂�
    [SerializeField] float lowJumpMultiplier = 2.0f;// ��W�����v����

    [Header("Death Count")]
    //[SerializeField] int maxDeathCount = 100;   // ���񎀂񂾂�Q�[���I�[�o�[
    private static int deathCount = 0;        // ����̎��S��
    private static int totalDeathCount = 0;   // �g�[�^�����S��
    private static bool loaded = false;       // ��1�񂾂����[�h�p


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
        // ���ŏ���1�񂾂��ۑ��f�[�^��ǂ�
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

        // �W�����v
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            animator.SetTrigger("JumpTrigger");
            rigid.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            if (jumpSE != null)
                audioSource.PlayOneShot(jumpSE);

            isGrounded = false;
        }

        // ���[���ړ�
        if (Input.GetKeyDown(KeyCode.RightArrow))
            currentLane = Mathf.Min(currentLane + 1, 2);

        if (Input.GetKeyDown(KeyCode.LeftArrow))
            currentLane = Mathf.Max(currentLane - 1, 0);

        // �ړ�
        Vector3 pos = transform.position;
        pos.x = Mathf.Lerp(pos.x, positions[currentLane], Time.deltaTime * laneMoveSpeed);
        pos.z += forwardSpeed * Time.deltaTime;
        transform.position = pos;

        // �X�s�[�h�A�b�v
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
         //�����~�X
        if (transform.position.y < -5f)
        {
            Miss();
        }
    }

    void FixedUpdate()
    {
        // �����𑬂����ăW�����v���C�����悭
        if (rigid.linearVelocity.y < 0)
        {
            rigid.linearVelocity += Vector3.up * Physics.gravity.y
                * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }
        // ��W�����v����
        else if (rigid.linearVelocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            rigid.linearVelocity += Vector3.up * Physics.gravity.y
                * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // ���n����i�������̂݁j
        if (collision.gameObject.CompareTag("Ground"))
        {
            foreach (ContactPoint contact in collision.contacts)
            {
                if (contact.normal.y > 0.5f)
                {
                    isGrounded = true;
                    //animator.ResetTrigger("JumpTrigger");
                    break;
                }
            }
        }

        if (collision.gameObject.CompareTag("out"))
        {
            SceneManager.LoadScene("GameOver 1");
        }
    }

    // ======================
    // ��_������
    // ======================
    void Miss()
    {

        SceneManager.LoadScene("GameOver 1");
        //if (dead) return;

        //dead = true;
        //deathCount++;   // �� ���S�񐔂𑝂₷
        //totalDeathCount++;   // ���g�[�^��

        //if (damageSE != null)
        //    audioSource.PlayOneShot(damageSE);

        //Debug.Log("���S�񐔁F" + deathCount);
        //Debug.Log("�g�[�^�����S�񐔁F" + totalDeathCount);
        //if (deathCount >= maxDeathCount)
        //{
        //    SceneManager.LoadScene("GameOver");
        //    return;
        //}

        //animator.SetBool("Fall", true);
        //StartCoroutine(RestartStage());
    }

    IEnumerator RestartStage()
    {
        yield return new WaitForSeconds(2f);
        dead = false;   // �� ���d�v
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

    // ====== ���񕪂������Z�b�g ======
    public static void ResetDeathCount()
    {
        deathCount = 0;
    }
}
