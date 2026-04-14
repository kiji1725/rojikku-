using UnityEngine;

public class BreakFloor : MonoBehaviour
{
    Rigidbody[] pieces;
    bool isBroken = false;
    void Start()
    {
        pieces = GetComponentsInChildren<Rigidbody>();

        // ç≈èâÇÕå≈íË
        foreach (Rigidbody rb in pieces)
        {
            rb.isKinematic = true;
        }
    }

    //Update is called once per frame
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        Break();
    //    }

    //}
    /*void Update()
    {
       if (Input.GetKeyDown(KeyCode.Space))
       {
           Break();
       }
    }*/



    public void Break()
    {
        if (isBroken) return;
        isBroken = true;
        foreach (Rigidbody rb in pieces)
        {
            rb.isKinematic = false;
            rb.AddForce(Random.onUnitSphere * 3f, ForceMode.Impulse);
        }

        Destroy(gameObject, 3f);
    }

}
