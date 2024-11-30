using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private Transform target;
    [SerializeField] private float damage = 3f;

    // Start is called before the first frame update
    void Start()
    {
        // Set hunter as a target
        GameObject hunterObject = GameObject.FindWithTag("Hunter");
        if (hunterObject != null)
        {
            SetTarget(hunterObject.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            transform.Translate(direction * speed * Time.deltaTime, Space.World);
        }
    }

    // Death of monster
    public void Death()
    {
        Destroy(gameObject);
    }

    // Set a target for a monster
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    public float GetDamage()
    {
        return damage;
    }
}
