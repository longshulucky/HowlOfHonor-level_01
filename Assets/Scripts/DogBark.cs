using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogBark : MonoBehaviour
{
    public LayerMask monsterLayer;
    [SerializeField] private Transform hunter;
    [SerializeField] private float barkRadius = 20f;
    [SerializeField] private float chasingTime = 10f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Bark());
        }
    }

    IEnumerator Bark()
    {
        Collider[] monsters = Physics.OverlapSphere(transform.position, barkRadius, monsterLayer);
        List<Monster> affectedMonsters = new List<Monster>();

        foreach (Collider monsterCollider in monsters)
        {
            Monster monster = monsterCollider.GetComponent<Monster>();
            if (monster != null)
            {
                monster.SetTarget(transform); // Player is a target
                affectedMonsters.Add(monster);
            }
        }

        yield return new WaitForSeconds(chasingTime);

        // Target is a hunter
        foreach (Monster monster in affectedMonsters)
        {
            if (monster != null)
            {
                monster.SetTarget(hunter);
            }
        }
    }
}
