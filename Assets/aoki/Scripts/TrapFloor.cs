using UnityEngine;

public class TrapFloor : MonoBehaviour
{

    public float rotateSpeed = 200f;
    private bool isActivated = false;
    private Quaternion targetRotation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targetRotation = transform.rotation * Quaternion.Euler(180f, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (isActivated)
        {
            transform.rotation = Quaternion.RotateTowards
                (transform.rotation,targetRotation, rotateSpeed * Time.deltaTime);
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
