using UnityEngine;

public class PickUp : MonoBehaviour
{
    // --- ’e”ŠÇ— ---
    public int currentAmmo = 0;
    public int maxAmmo = 50;

    // --- ’e‚ğ‘‚â‚· ---
    public void AddAmmo(int amount)
    {
        currentAmmo += amount;

        if (currentAmmo > maxAmmo)
            currentAmmo = maxAmmo;

        Debug.Log("Œ»İ‚Ì’e”: " + currentAmmo);
    }

    // --- ’e‚ğg‚¤ ---
    public bool UseAmmo(int amount)
    {
        if (currentAmmo >= amount)
        {
            currentAmmo -= amount;
            return true;
        }

        return false;
    }

    // --- E‚¤ˆ— ---
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ammo"))
        {
            AddAmmo(1);
            Destroy(other.gameObject);
        }
    }
}