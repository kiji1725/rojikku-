using UnityEngine;

public class RotateItem : MonoBehaviour
{

    float startPos;
    const float speed = 2.0f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position.y;


    }

    // Update is called once per frame
    void Update()
    {

        // —‚¿‚Ä‚¢‚éƒAƒCƒeƒ€‚Ìã‰ºˆÚ“®‚Æ‰ñ“]
        // ‰ºŒÀ0.5ãŒÀ1.0
        float posY = startPos + Mathf.Sin(Time.time * speed) * 0.5f;

        transform.position = new Vector3(transform.position.x, posY, transform.position.z);
        transform.Rotate(0, 0, 90 * Time.deltaTime * 0.75f);

    }
}
