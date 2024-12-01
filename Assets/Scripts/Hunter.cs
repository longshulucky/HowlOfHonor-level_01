using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunter : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float changeDirectionTime = 3f;
    [SerializeField] private float timer; // Change direction timer
    [SerializeField] private Vector3 direction;

    [SerializeField] private float health = 100f;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private ItemDisplay itemDisplay;

    [SerializeField] private Weapon weapon;
    private float attackTimer = 0f;

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
        attackTimer -= Time.deltaTime;

        if (timer < 0)
        {
            ChangeDirection();
        }
        transform.Translate(direction * speed * Time.deltaTime, Space.World);

        // Attack if hunter has a weapon
        if (weapon != null && attackTimer <= 0)
        {
            Attack();
            attackTimer = weapon.GetCooldownTime();
        }
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

    public void UseHealthPotion(HealthPotion potion)
    {
        health += potion.GetHealthAmount();
        if (health > 100f) health = 100f;
        healthBar.UpdateHealth(health);
        potion.Consume();
    }

    public bool IsCarryingWeapon()
    {
        return weapon != null;
    }

    public void EquipWeapon(Weapon weapon)
    {
        this.weapon = weapon;
        attackTimer = weapon.GetCooldownTime(); // Start with cooldown time
        weapon.transform.SetParent(transform); // Set hunter as a parent
        weapon.transform.localPosition = new Vector3(0, 1, 0); // Position for carrying weapon
        itemDisplay.UpdateItemUI(weapon.GetWeaponIcon(), attackTimer);
    }

    void Attack()
    {
        if (weapon == null) return;
        Collider[] monsters = Physics.OverlapSphere(transform.position, weapon.GetAttackRadius());
        if (monsters.Length == 0) return;

        StartCoroutine(RemoveWeapon());
        foreach (Collider monsterCollider in monsters)
        {
            Monster monster = monsterCollider.GetComponent<Monster>();
            if (monster != null)
            {
                monster.LoseHealth(weapon.GetDamage());
            }
        }
    }

    IEnumerator RemoveWeapon()
    {
        yield return new WaitForSeconds(weapon.GetUseTime());
        //itemDisplay.UpdateItemUI(weapon.GetWeaponIcon(), weapon.GetUseTime());
        if (weapon != null)
        {
            Destroy(weapon.gameObject);
            weapon = null;
            itemDisplay.ClearItemUI();
        }
    }

    public float GetHealth()
    {
        return health;
    }
}
