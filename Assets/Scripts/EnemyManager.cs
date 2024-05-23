using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    public Enemy[] allEnemies;

    // Start is called before the first frame update
    void Start()
    {
        UpdateEnemyList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateEnemyList() {
        allEnemies = FindObjectsOfType<Enemy>();
        //Debug.Log(allEnemies.Length);
    }

}
