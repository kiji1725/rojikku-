using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform player;                
    public Vector3 offset = new Vector3(0f, 1.4f, -6f);  
    public Vector3 rotation = new Vector3(50f, 0f, 0f);
    
    private float fixedX; 
    void Start()
    {
       

       
        fixedX = player.position.x + offset.x;

        transform.position = new Vector3(fixedX, player.position.y + offset.y, player.position.z + offset.z);
        transform.LookAt(player);
    }

    void Update()
    {
        if (player == null) return;

       
        transform.position = new Vector3(
            fixedX,
            player.position.y + offset.y,
            player.position.z + offset.z
        );

        transform.LookAt(player.position + Vector3.up * 2f);
    }
}
