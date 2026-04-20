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
        transform.position += move * speed * Time.deltaTime;
    }
    void Update()
    {
        moveX = 0.0f;
        moveZ = 0.0f;

        /*if (Input.GetKey(KeyCode.T))*/ moveZ += 1f;
        //if (Input.GetKey(KeyCode.G)) moveZ -= 1f;
        //if (Input.GetKey(KeyCode.F)) moveX -= 1f;
        //if (Input.GetKey(KeyCode.H)) moveX += 1f;
        
        move = new Vector3(moveX, 0, moveZ).normalized;

        if (transform.position.y < -5f)
        {
            SceneManager.LoadScene("GameOver 1");
        }

    }
    
}
