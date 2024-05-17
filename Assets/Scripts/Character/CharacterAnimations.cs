using UnityEngine;

public class CharacterAnimations : MonoBehaviour
{
    private Animator animator;

    private int walking;
    private int jumping;
    private int grounded;
    private int punch1;
    private int punch2;
    private int punch3;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        walking = Animator.StringToHash(AnimationTags.WALKING);
        jumping = Animator.StringToHash(AnimationTags.JUMPING);
        grounded = Animator.StringToHash(AnimationTags.GROUNDED);
        punch1 = Animator.StringToHash(AnimationTags.PUNCH_1_TRIGGER);
        punch2 = Animator.StringToHash(AnimationTags.PUNCH_2_TRIGGER);
        punch3 = Animator.StringToHash(AnimationTags.PUNCH_3_TRIGGER);
    }

    public void Walk(bool move)
    {
        SetAnimationBool(move, walking);
    }

    public void Jump()
    {
        animator.SetTrigger(jumping);
    }

    public void Grounded(bool isGrounded) 
    {
        SetAnimationBool(isGrounded, grounded);
    }

    public void Punch_1()
    {
        animator.SetTrigger(punch1);
    }

    public void Punch_2()
    {
        animator.SetTrigger(punch2);
    }

    public void Punch_3()
    {
        animator.SetTrigger(punch3);
    }

    private void SetAnimationBool(bool state, int hash)
    {
        bool isActive = animator.GetBool(hash);

        if(!isActive && state) animator.SetBool(hash, state);
        else if(isActive && !state) animator.SetBool(hash, state);
    }
}
