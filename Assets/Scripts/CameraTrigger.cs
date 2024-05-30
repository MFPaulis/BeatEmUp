using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    public bool isEnemy = false;
    // Start is called before the first frame update
    void Start()
    {
        isEnemy = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other) {
        //Debug.Log(other);
		if (other.tag == "Enemy") {
			isEnemy = true;
            //Debug.Log("ENEMY");
		}
	}
}
