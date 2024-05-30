using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    public Enemy[] allEnemies;
    GameObject character;

    // Start is called before the first frame update
    void Start()
    {
        UpdateEnemyList();
        character = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
       int chosen = 0;
       float minDistance = Vector3.Distance(allEnemies[0].transform.position, character.transform.position);
       for(int i = 0; i < allEnemies.Length; i++)
       {
            float distance = Vector3.Distance(allEnemies[i].transform.position, character.transform.position);
            if(distance < minDistance)
            {
                minDistance = distance;
                chosen = i;
            }
       }
       for (int i = 0; i < allEnemies.Length; i++)
       {
            allEnemies[i].isFighting = false;
       }
       allEnemies[chosen].isFighting = true;
    }

    public void UpdateEnemyList() {
        allEnemies = FindObjectsOfType<Enemy>();
        //Debug.Log(allEnemies.Length);
    }

}
