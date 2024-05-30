using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum TypeOfAttack
{
    none, punch, kick
};

public class AttackSystem : MonoBehaviour
{
    BoxCollider colliderPunch;
    Animator animator;
    [SerializeField] bool isPlayer;

    public bool isPunching = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        GameObject colliderPunchObject = transform.Find("ColliderPunch").gameObject;
        colliderPunch = colliderPunchObject.GetComponent<BoxCollider>();
        colliderPunch.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetAttacked(int damageAmount)
    {
        if (isPlayer)
        {
            gameObject.GetComponent<CharacterHealth>().Damage(damageAmount);
        }
        else
        {
            gameObject.GetComponent<EnemyHealth>().Damage(damageAmount);
            gameObject.GetComponent<Enemy>().Stun();
        }
        
    }

    

    public void StartPunch()
    {
        isPunching = true;
        colliderPunch.gameObject.SetActive(true);
    }

    public void EndPunch()
    {
        isPunching = false;
        colliderPunch.gameObject.GetComponent<AttackBox>().collided = false;
        colliderPunch.gameObject.SetActive(false);
    }


    public void Attack(TypeOfAttack attack) {
        //Play animation
        switch(attack) {
            case TypeOfAttack.punch:
                animator.SetTrigger("Attack2");
                break;
            case TypeOfAttack.kick:
                animator.SetTrigger("Attack3");
                break;
        }
        //play sound
    }

}
