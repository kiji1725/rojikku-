using UnityEngine;

public class ColliderController : MonoBehaviour
{
    [SerializeField] private CapsuleCollider capsule;
    [SerializeField] private PlayerMotionController playerMotion;

    void Start()
    {
        
    }




    void Update()
    {

        if (playerMotion.isSliding)
        {

            capsule.direction = 2;
            capsule.center = new Vector3(0.0f, 0.4f, 0.0f);

        }

        if (!playerMotion.isSliding)
        {

            capsule.direction = 1;
            capsule.center = new Vector3(0.0f, 0.84f, 0.0f);

        }



    }
}
