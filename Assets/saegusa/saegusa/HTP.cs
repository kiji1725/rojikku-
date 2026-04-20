using UnityEngine;

public class HTP : MonoBehaviour
{
    public GameObject targetImage;

    public void Show()
    {
        targetImage.SetActive(true);
    }
    public void Toggle()
    {
        targetImage.SetActive(!targetImage.activeSelf);
    }
}