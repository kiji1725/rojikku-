using UnityEngine;

public class BreakTrigger : MonoBehaviour
{
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponentInParent<BreakFloor>().Break();
        }
    }
}
