using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    // ステージ上でプレイヤーを移動させるためのスピード
    [SerializeField] float speed = 5.0f;

    float moveX = 0.0f;
    float moveZ = 0.0f;

    Vector3 move;

    void Start()
    {
        move = new Vector3(moveX, 0, moveZ).normalized;

    }
    private void FixedUpdate()
    {
        transform.position += speed * Time.deltaTime * move;
    }
    void Update()
    {
        moveX = 0.0f;
        moveZ = 0.0f;

        moveZ += 1f;
        
        move = new Vector3(moveX, 0, moveZ).normalized;

        if (transform.position.y < -5.0f)
        {
            SceneManager.LoadScene("GameOver");
        }

    }
    
    public Vector3 PlayerPos { get { return transform.position; } }

}
