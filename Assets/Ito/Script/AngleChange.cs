using UnityEngine;



public class AngleChange : MonoBehaviour
{
    [SerializeField] private Animator animator;

    // string GR = "GunRun";

    public float maxAngle = 90f;
    public float stepAngle = 45f;

    
    float currentZ = 0f;

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        currentZ = Mathf.Min(currentZ + stepAngle, maxAngle);

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        currentZ = Mathf.Max(currentZ - stepAngle, -maxAngle);


        transform.rotation = Quaternion.Euler(0, 0, currentZ);
    
        // •Ç‚Ì–â‘è‚ª‰ðŒˆ‚µ‚½‚çRaycast‚Å”»’è‚µ‚Ä•Ç‚ª‚ ‚é‚Æ‚±‚ë‚¾‚¯‘–‚ê‚é‚æ‚¤‚É‚·‚é
        


        
    
    }
}