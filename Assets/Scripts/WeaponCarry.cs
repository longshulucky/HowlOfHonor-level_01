using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCarry : MonoBehaviour
{
    [SerializeField] private Transform carryPoint; // Point of holding the weapon
    [SerializeField] private Weapon carriedWeapon;
    [SerializeField] private float pickupRadius = 2f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // Collect the weapon
        {
            TryPickupWeapon();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hunter") && !other.GetComponent<Hunter>().IsCarryingWeapon())
        {
            GiveWeaponToHunter();
        }
    }

    void TryPickupWeapon()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, pickupRadius);
        foreach (Collider collider in colliders)
        {
            Weapon weapon = collider.GetComponent<Weapon>();
            if (weapon != null && carriedWeapon == null)
            {
                PickupWeapon(weapon);
                break;
            }
        }
    }

    void PickupWeapon(Weapon weapon)
    {
        carriedWeapon = weapon;
        weapon.transform.SetParent(carryPoint);
        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localRotation = Quaternion.identity;
    }

    void GiveWeaponToHunter()
    {
        Hunter hunter = FindObjectOfType<Hunter>();
        if (hunter != null)
        {
            if (!hunter.IsCarryingWeapon())
            {
                hunter.EquipWeapon(carriedWeapon);
                carriedWeapon = null;
            }
        }
    }
}
