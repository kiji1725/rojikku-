using UnityEngine;

public class HTP : MonoBehaviour
{
    public GameObject targetImage;

    [Header("SE")]
    public AudioSource audioSource;
    public AudioClip buttonSE;

    public void Show()
    {
        PlaySE();

        if (targetImage != null)
        {
            targetImage.SetActive(false);
        }
    }

    public void Hide()
    {
        PlaySE();

        if (targetImage != null)
        {
            targetImage.SetActive(false);
        }
    }

    public void Toggle()
    {
        PlaySE();

        if (targetImage != null)
        {
            targetImage.SetActive(!targetImage.activeSelf);
        }
    }

    void PlaySE()
    {
        if (audioSource != null && buttonSE != null)
        {
            audioSource.PlayOneShot(buttonSE);
        }
    }
}