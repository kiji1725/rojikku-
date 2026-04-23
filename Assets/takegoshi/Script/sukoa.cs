using UnityEngine;

public class sukoa : MonoBehaviour
{

    [SerializeField] private PlayerMove playerMove;

    float score = 0;

    void Start()
    {
        
    }

    
    void Update()
    {
        
        score = playerMove.PlayerPos.z;

    }


}
