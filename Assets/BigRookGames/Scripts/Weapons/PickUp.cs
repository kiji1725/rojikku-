//using Unity.Android.Gradle.Manifest;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    // --- íeêîä«óù ---
    public int currentAmmo = 0;
    public int maxAmmo = 50;

    public Animation Animation = null;

    // SE
    [Header("SE")]
    public AudioSource audioSource;
    public AudioClip AmmoSE;

    // --- íeÇëùÇ‚Ç∑ ---
    public void AddAmmo(int amount)
    {
        currentAmmo += amount;

        if (currentAmmo > maxAmmo)
            currentAmmo = maxAmmo;

        Debug.Log("åªç›ÇÃíeêî: " + currentAmmo);

        audioSource.PlayOneShot(AmmoSE);

    }

    // --- íeÇégÇ§ ---
    public bool UseAmmo(int amount)
    {

        if (currentAmmo >= amount)
        {
            currentAmmo -= amount;
            return true;
        }

        if (Animation != null)
        {

            Animation.Play();

        }

        return false;
    }

    // --- èEÇ§èàóù ---
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ammo"))
        {
            AddAmmo(1);
            Destroy(other.gameObject);
        }
    }
}