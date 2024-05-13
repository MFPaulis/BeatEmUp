using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{

    [SerializeField] public GameObject target;
    [SerializeField] float movingSpeed;
    [SerializeField] float attackSpeed;
    [SerializeField] float minAttackDistance;
    [SerializeField] float maxAttackDistance;
    [SerializeField] float minDistance;
    [SerializeField] float maxDistance;
    [SerializeField] bool isSpawned = true;

    private bool attackBack = false;

    //for spawned enemies
    private float countdown = 5f;
    private EnemySpawner enemySpawner;

    // Start is called before the first frame update
    void Start()
    {   
        if (isSpawned) {
            enemySpawner = GetComponentInParent<EnemySpawner>();
            //Debug.Log(enemySpawner);
        }
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

        //for spawned enemies
        if (isSpawned) {
            countdown -= Time.deltaTime;

            if (countdown <= 0)
            {

                Destroy(gameObject);

                enemySpawner.waves[enemySpawner.currentWaveIndex].enemiesLeft--;
                
            }
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
