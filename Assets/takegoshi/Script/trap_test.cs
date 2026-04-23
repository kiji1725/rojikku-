using UnityEngine;

public class trap_test : MonoBehaviour
{

    public float rotateSpeed = 200f;
    private bool isActivated = false;
    private Quaternion targetRotation;



    void Start()
    {
        targetRotation = transform.rotation * Quaternion.Euler(180f, 0f, 0f);
    }


    void Update()
    {
        if (isActivated)
        {
            transform.rotation = Quaternion.RotateTowards
                (transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isActivated = true;
        }
    }
}
