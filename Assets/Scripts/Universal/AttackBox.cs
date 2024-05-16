using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBox : MonoBehaviour
{
    bool isPlayer;
    AttackSystem attackSystem;
    public bool collided = false;
    // Start is called before the first frame update
    void Start()
    {
        attackSystem = transform.parent.GetComponent<AttackSystem>();
        if (transform.parent.tag == "Player")
        {
            isPlayer = true;
        } else
        {
            isPlayer = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider col)
    {
        if (!collided && col.tag == "Player" && !isPlayer && attackSystem.isPunching)
        {
            col.gameObject.GetComponent<AttackSystem>().GetAttacked(5);
            collided = true;
        }
    }
}
