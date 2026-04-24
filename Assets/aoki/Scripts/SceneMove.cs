using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMove : MonoBehaviour
{
    public void GoTitle()
    {
        FadeManager.Instance.LoadSceneWithFade("Title");
    }
}
