using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class Change : MonoBehaviour
{
    [SerializeField] private Material dissolveMat;
    [SerializeField] private float speed = 1.5f;

    bool isTransitioning = false;

    void Start()
    {
        // シーン開始時は見える状態に戻す
        dissolveMat.SetFloat("_Cutoff", 0f);
    }

    void Update()
    {
        // Enterキーでシーン切り替え
        if (!isTransitioning && Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(DissolveAndChangeScene("2"));
        }
    }

    IEnumerator DissolveAndChangeScene(string sceneName)
    {
        isTransitioning = true;

        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * speed;
            dissolveMat.SetFloat("_Cutoff", t);
            yield return null;
        }

        SceneManager.LoadScene(sceneName);
    }
}
