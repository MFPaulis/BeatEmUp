using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Start is called before the first frame update
    //https://www.youtube.com/watch?v=duo45NjwZ78

    [SerializeField] private float countdown;
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] GameObject target;
    [SerializeField] GameObject levelArea;

    
    public Wave[] waves;
    public int currentWaveIndex = 0;

    private bool readyToCountDown;
    private Bounds levelAreaBounds;

    void Start()
    {
        levelAreaBounds = levelArea.GetComponent<Collider>().bounds;
        readyToCountDown = true;

        for(int i = 0; i < waves.Length; i++) 
        {
            waves[i].enemiesLeft = waves[i].enemies.Length;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentWaveIndex >= waves.Length) 
        {
            //Debug.Log("You survived every wave");
            return;
        }

        if (readyToCountDown == true) 
        {
            countdown -= Time.deltaTime;
        }

        if(countdown <= 0) 
        {
            readyToCountDown = false;
            countdown = waves[currentWaveIndex].timeToNextWave;
            StartCoroutine(SpawnWave());
            
        }

        if (waves[currentWaveIndex].enemiesLeft == 0)
        {
            readyToCountDown = true;
            currentWaveIndex++;
        }
    }

    private IEnumerator SpawnWave() 
    {
        if (currentWaveIndex < waves.Length) {
            for (int i = 0; i < waves[currentWaveIndex].enemies.Length; i++) {
                waves[currentWaveIndex].enemies[i].target = target;
                
                //getting random spawn point
                float randomX = Random.Range(10f, 15f);
                if(target.transform.position.x + randomX > levelAreaBounds.max.x) {
                    if(target.transform.position.x - randomX < levelAreaBounds.min.x) {
                        randomX = target.transform.position.x + 5f;
                    } 
                    else {
                        randomX = target.transform.position.x - randomX;
                    }
                } 
                else {
                    randomX = target.transform.position.x + randomX;
                }
                float randomY = levelAreaBounds.max.y + 1f;
                float randomZ = Random.Range(levelAreaBounds.min.z, levelAreaBounds.max.z);
                Vector3 enemyPosition = new Vector3(randomX, randomY, randomZ);

                Enemy enemy = Instantiate(waves[currentWaveIndex].enemies[i], spawnPoint.transform);
                enemy.transform.position = enemyPosition;
                enemy.transform.SetParent(spawnPoint.transform);

                yield return new WaitForSeconds(waves[currentWaveIndex].timeToNextEnemy);
            }
        }
    }
}


[System.Serializable]

public class Wave
{
    public Enemy[] enemies;
    public float timeToNextEnemy;
    public float timeToNextWave;

    [HideInInspector] public int enemiesLeft;
}


