using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private Transform target;
    [SerializeField] private float damage = 3f;
    [SerializeField] private float attackDistance = 3f;
    [SerializeField] private bool isAttacking = false;

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
        if (target != null && !isAttacking)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            float distance = Vector3.Distance(transform.position, target.position);

            if (distance > attackDistance)
            {
                transform.Translate(direction * speed * Time.deltaTime, Space.World);
            }
            else
            {
                StartCoroutine(Attack());
            }
        }
    }

    private IEnumerator Attack()
    {
        isAttacking = true;

        Vector3 attackPosition = target.position + new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
        Vector3 startPosition = transform.position;
        float jumpTime = 0.5f;
        float timer = 0;

        while (timer < jumpTime)
        {
            timer += Time.deltaTime;
            transform.position = Vector3.Lerp(startPosition, attackPosition, timer / jumpTime);
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        // Jump back
        timer = 0;
        while (timer < jumpTime)
        {
            timer += Time.deltaTime;
            transform.position = Vector3.Lerp(attackPosition, startPosition, timer / jumpTime);
            yield return null;
        }

        isAttacking = false;
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
