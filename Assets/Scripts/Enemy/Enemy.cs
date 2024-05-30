using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    
    [SerializeField] public GameObject target;
    [SerializeField] float movingSpeed;
    [SerializeField] float attackSpeed;
    [SerializeField] float attackDistance;
    [SerializeField] float minDistance;
    [SerializeField] float maxDistance;
    [SerializeField] bool isSpawned = true;
    [SerializeField] float stunTime = 1.5f;


    private bool attackBack = false;
    private bool attacking = false;

    private AttackSystem attackSystem;
    private Animator animator;

    //for spawned enemies
    private float countdown = 5f;
    private EnemySpawner enemySpawner;

    private bool isStunned = false;

    HealthSystem healthSystem;

    // Start is called before the first frame update
    void Start()
    {   
        attackSystem = GetComponent<AttackSystem>();
        animator = GetComponent<Animator>();
       
        if (isSpawned) {
            enemySpawner = GetComponentInParent<EnemySpawner>();
            //Debug.Log(enemySpawner);
        }
    }

    public void Setup(HealthSystem healthSystem)
    {
        this.healthSystem = healthSystem;
        healthSystem.OnDeath += Die;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = (transform.position - target.transform.position).magnitude;
        if (!isStunned)
        {
            if (distance < attackDistance)
            {
                Attack(distance);
            }
            else if (distance < maxDistance)
            {
                attackBack = false;
                ChasePlayer(movingSpeed);
            }
            else
            {
                attackBack = false;
            }
        }
        
        //for spawned enemies
        if (isSpawned) {
            countdown -= Time.deltaTime;

            if (countdown <= 0)
            {

                //Destroy(gameObject);

                //enemySpawner.waves[enemySpawner.currentWaveIndex].enemiesLeft--;
                
            }
        }

    }

    public void AlertObservers(string message)
    {
        if (message.Equals("AttackAnimationEnded"))
        {
            attackBack = true;
            attacking = false;
            animator.SetTrigger("NotAttack");
        } 
    }

    void Attack(float distance)
    {
        if (!attacking)
        {
            if (attackBack)
            {
                ChasePlayer(-attackSpeed);
            }
            else
            {
                ChasePlayer(attackSpeed);
            }
        }
        if (distance < minDistance && !attackBack && !attacking)
        {
            attackSystem.Attack(TypeOfAttack.punch);
            attacking = true;
        } 
    }

    void ChasePlayer(float speed)
    {
        Vector2 enemyPos = new Vector2(transform.position.x, transform.position.z);
        Vector2 playerPos = new Vector2(target.transform.position.x, target.transform.position.z);
        Vector2 difference = playerPos - enemyPos;
        RotateEnemy(difference.x);
        enemyPos += (difference.normalized * speed * Time.deltaTime);
        Vector3 newPos = new Vector3(enemyPos.x, transform.position.y, enemyPos.y);
        transform.position = newPos;
    }

    void RotateEnemy(float movingDirection)
    {
        if (movingDirection < 0)
        {
            transform.localRotation = Quaternion.Euler(0, -90, 0);
        } else
        {
            transform.localRotation = Quaternion.Euler(0, 90, 0);
        }
    }

    public void Stun()
    {
        isStunned = true;
        StartCoroutine(DisableStun());
    }

    IEnumerator DisableStun()
    {
        yield return new WaitForSeconds(stunTime);
        isStunned = false;
    }

    private void Die(object sender, System.EventArgs e)
    {
        Destroy(gameObject);
        enemySpawner.waves[enemySpawner.currentWaveIndex].enemiesLeft--;
    }
}
