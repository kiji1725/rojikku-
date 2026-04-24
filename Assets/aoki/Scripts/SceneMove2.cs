using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMove2 : MonoBehaviour
{
    public void GoSelect()
    {
        FadeManager.Instance.LoadSceneWithFade("StageSelect 1");
    }
}
