using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCarry : MonoBehaviour
{
    [SerializeField] private Transform carryPoint; // Point of holding the item
    [SerializeField] private Weapon carriedWeapon;
    [SerializeField] private HealthPotion carriedPotion;
    [SerializeField] private float pickupRadius = 2f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // Collect the item
        {
            TryPickupItem();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hunter"))
        {
            Hunter hunter = other.GetComponent<Hunter>();
            if (hunter != null)
            {
                if (carriedWeapon != null && !hunter.IsCarryingWeapon())
                {
                    GiveWeaponToHunter(hunter);
                }
                else if (carriedPotion != null)
                {
                    GivePotionToHunter(hunter);
                }
            }
        }
    }

    void TryPickupItem()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, pickupRadius);
        foreach (Collider collider in colliders)
        {
            if (carriedWeapon == null && carriedPotion == null)
            {
                Weapon weapon = collider.GetComponent<Weapon>();
                if (weapon != null)
                {
                    PickupWeapon(weapon);
                    break;
                }

                HealthPotion potion = collider.GetComponent<HealthPotion>();
                if (potion != null)
                {
                    PickupPotion(potion);
                    break;
                }
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

    void PickupPotion(HealthPotion potion)
    {
        carriedPotion = potion;
        potion.transform.SetParent(carryPoint);
        potion.transform.localPosition = Vector3.zero;
        potion.transform.localRotation = Quaternion.identity;
    }

    void GiveWeaponToHunter(Hunter hunter)
    {
        hunter.EquipWeapon(carriedWeapon);
        carriedWeapon = null;
    }

    void GivePotionToHunter(Hunter hunter)
    {
        hunter.UseHealthPotion(carriedPotion);
        carriedPotion = null;
    }
}
