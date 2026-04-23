using UnityEngine;

public class FireWave : MonoBehaviour
{

    public GameObject fireballPrefab;
    public int count = 6;
    public float spacing = 0.7f;

    public float waveHeight = 1.0f;
    public float waveSpeed = 2.0f;
    public float waveOffset = 0.5f;

    GameObject[] balls;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        balls = new GameObject[count];

        for (int i = 0; i < count; i++)
        {
            Vector3 pos = new Vector3(spacing * i, 0, 0);
            GameObject obj = Instantiate(fireballPrefab, transform);
            obj.transform.localPosition = pos;

            balls[i] = obj;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0;i < balls.Length;i++)
        {
            float y = Mathf.Sin(Time.time * waveSpeed + i * waveOffset) * waveHeight;

            Vector3 pos = balls[i].transform.localPosition;
            pos.y = y;
            balls[i].transform.localPosition = pos;
        }
    }
}
