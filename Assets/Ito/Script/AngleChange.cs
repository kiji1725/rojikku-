using UnityEngine;


public class AngleChange : MonoBehaviour
{
    public float maxAngle = 90f;
    public float stepAngle = 45f;

    float currentZ = 0f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
            currentZ = Mathf.Min(currentZ + stepAngle, maxAngle);

        if (Input.GetKeyDown(KeyCode.LeftArrow))
            currentZ = Mathf.Max(currentZ - stepAngle, -maxAngle);

        transform.rotation = Quaternion.Euler(0, 0, currentZ);
    }
}