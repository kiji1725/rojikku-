using UnityEngine;

public class Score : MonoBehaviour
{

     [SerializeField] float score = 0;


    void Start()
    {
        
    }


    void Update()
    {
        
        score = transform.position.z;




    }
}
