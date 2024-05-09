using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{

    [SerializeField] GameObject target;
    [SerializeField] float movingSpeed;
    [SerializeField] float attackSpeed;
    [SerializeField] float minAttackDistance;
    [SerializeField] float maxAttackDistance;
    [SerializeField] float minDistance;
    [SerializeField] float maxDistance;

    private bool attackBack = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float distance = (transform.position - target.transform.position).magnitude;
        if ( distance < maxAttackDistance)
        {
            Attack(distance);
        } else if (distance < maxDistance)
        {
            ChasePlayer(movingSpeed);
        }
    }

    void Attack(float distance)
    {
        if (distance > minAttackDistance)
        {
            attackBack = false;
        }
        if (attackBack)
        {
            ChasePlayer(-attackSpeed);
        } else
        {
            ChasePlayer(attackSpeed);
        }
        if (distance < minDistance)
        {
            attackBack = true;
        } 
    }

    void ChasePlayer(float speed)
    {
        Vector2 enemyPos = new Vector2(transform.position.x, transform.position.z);
        Vector2 playerPos = new Vector2(target.transform.position.x, target.transform.position.z);
        Vector2 difference = playerPos - enemyPos;
        enemyPos += (difference.normalized * speed * Time.deltaTime);
        Vector3 newPos = new Vector3(enemyPos.x, transform.position.y, enemyPos.y);
        transform.position = newPos;
    }
}
