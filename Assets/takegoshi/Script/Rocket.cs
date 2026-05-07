using UnityEngine;
using BigRookGames.Weapons;

public class Rocket : MonoBehaviour
{

    [SerializeField] private GunfireController gun;

    public void FireWeapon()
    {
        gun.FireWeapon();
    }



}
