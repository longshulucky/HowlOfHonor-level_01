using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float damage = 2f;
    [SerializeField] private float attackRadius = 5f;
    [SerializeField] private float useTime = 4f;
    [SerializeField] private float attackCooldown = 1f;

    public float GetDamage()
    {
        return damage;
    }

    public float GetAttackRadius()
    {
        return attackRadius;
    }

    public float GetUseTime()
    {
        return useTime;
    }
}
