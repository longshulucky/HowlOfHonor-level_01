using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float attackRadius = 5f;
    [SerializeField] private float damage = 10f;
    [SerializeField] private float useTime = 3f; // Time after which the weapon is removed
    [SerializeField] private float cooldownTime = 1f; // Time between attacks

    public float GetAttackRadius() => attackRadius;
    public float GetDamage() => damage;
    public float GetUseTime() => useTime;
    public float GetCooldownTime() => cooldownTime;
}
