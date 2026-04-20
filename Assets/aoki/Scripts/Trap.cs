using UnityEngine;

public class Trap : MonoBehaviour
{
    public Transform spikes;   // トゲ（見た目のオブジェクト）
    public float height = 2f;  // 上にどれだけ出るか
    public float speed = 10f;  // 速さ

    private Vector3 startPos;
    private Vector3 endPos;
    private bool isActivated = false;

    void Start()
    {
        // 初期位置（下に埋まってる状態）
        startPos = spikes.localPosition;

        // 上に出た位置
        endPos = startPos + Vector3.up * height;
    }

    void Update()
    {
        if (isActivated)
        {
            spikes.localPosition = Vector3.MoveTowards(
                spikes.localPosition,
                endPos,
                speed * Time.deltaTime
            );
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("何か入った");

        if (other.CompareTag("Player"))
        {
            Debug.Log("プレイヤー入った");
            isActivated = true;
        }
    }

}
