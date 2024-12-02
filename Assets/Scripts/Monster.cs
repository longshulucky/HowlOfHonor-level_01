using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public LevelManager levelManager;
    [SerializeField] private float speed = 2f;
    [SerializeField] private Transform target;
    [SerializeField] private float damage = 3f;
    [SerializeField] private float attackDistance = 3f;
    [SerializeField] private bool isAttacking = false;
    [SerializeField] private float health = 10f;

    // Start is called before the first frame update
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
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

            // Rotate monster
            if (direction.magnitude > 0f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                // Повернуть только по оси Y
                if (targetRotation.eulerAngles.y > 90)
                {
                    transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                } else
                {
                    transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                }
                //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime); // Плавный поворот
            }

            if (distance > attackDistance)
            {
                transform.Translate(direction * speed * Time.deltaTime, Space.World);
            }
            else
            {
                StartCoroutine(Attack());
            }
        }

        if (health < 0)
        {
            Death();
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
        levelManager.OnMonsterKilled();
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

    public void LoseHealth(float damage)
    {
        health -= damage;
    }
}
