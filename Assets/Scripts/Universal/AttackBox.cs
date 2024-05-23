using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBox : MonoBehaviour
{
    bool isPlayer;
    AttackSystem attackSystem;
    CharacterAttack characterAttack;
    public bool collided = false;
    // Start is called before the first frame update
    void Start()
    {
        if (transform.parent.parent.tag == "Player")
        {
            isPlayer = true;
            attackSystem = transform.parent.parent.GetComponent<AttackSystem>();
            characterAttack = transform.parent.parent.GetComponent<CharacterAttack>();
        } else
        {
            isPlayer = false;
            attackSystem = transform.parent.GetComponent<AttackSystem>();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider col)
    {
        if (!collided)
        {
            if (col.tag == "Player" && !isPlayer && attackSystem.isPunching)
            {
                col.gameObject.GetComponent<AttackSystem>().GetAttacked(5);
                collided = true;
            }
            else if (col.tag == "Enemy" && isPlayer && characterAttack.IsSwordAttack())
            {
                col.gameObject.GetComponent<AttackSystem>().GetAttacked(5);
                collided = true;
            }
        }
        
    }
}
