using UnityEngine;



public class AngleChange : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerMotionController motionController;

    // string GR = "GunRun";

    public const float maxAngle = 90f;
    public const float stepAngle = 45f;

    
    float currentZ = 0f;

    void Update()
    {

        if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) && !motionController.JumpFlag)
        currentZ = Mathf.Min(currentZ + stepAngle, maxAngle);

        if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) && !motionController.JumpFlag)
        currentZ = Mathf.Max(currentZ - stepAngle, -maxAngle);


        transform.rotation = Quaternion.Euler(0, 0, currentZ);
    
        // •Ç‚Ì–â‘è‚ª‰ðŒˆ‚µ‚½‚çRaycast‚Å”»’è‚µ‚Ä•Ç‚ª‚ ‚é‚Æ‚±‚ë‚¾‚¯‘–‚ê‚é‚æ‚¤‚É‚·‚é
        

    
    }

    public float CurrentZ { get { return currentZ; } }



}