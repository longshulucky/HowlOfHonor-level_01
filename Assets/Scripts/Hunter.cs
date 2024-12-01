using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunter : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float changeDirectionTime = 3f;
    [SerializeField] private float timer;
    [SerializeField] private Vector3 direction;
    [SerializeField] private float health = 100f;
    [SerializeField] private HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        timer = changeDirectionTime;
        ChangeDirection();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            ChangeDirection();
        }
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    void ChangeDirection()
    {
        timer = changeDirectionTime;
        direction = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            Monster monster = other.GetComponent<Monster>();
            health -= monster.GetDamage();
            healthBar.UpdateHealth(health);
            if (health <= 0)
            {
                Debug.Log("Hunter is dead!");
            }
        }
    }
}
