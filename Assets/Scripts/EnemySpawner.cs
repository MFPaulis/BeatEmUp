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

    
    public Wave[] waves;
    public int currentWaveIndex = 0;

    private bool readyToCountDown;

    void Start()
    {
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
            Debug.Log("You survived every wave");
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
                
                Enemy enemy = Instantiate(waves[currentWaveIndex].enemies[i], spawnPoint.transform);
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


