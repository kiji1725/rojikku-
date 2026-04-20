using UnityEngine;

public class FireBar3D : MonoBehaviour
{

    public GameObject fireballPrefab;
    public int count = 6;
    public float spacing = 0.7f;
    public float rotationSpeed = 100f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 pos = new Vector3(spacing * i, 0 ,0);

            GameObject obj = Instantiate(fireballPrefab, transform);
            obj.transform.position = pos;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}
