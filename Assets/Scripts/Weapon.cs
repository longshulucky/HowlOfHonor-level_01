using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float damage = 2f;
    public void Attack() { }

    public float GetDamage()
    {
        return damage;
    }
}
