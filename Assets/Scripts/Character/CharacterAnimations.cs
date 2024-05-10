using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimations : MonoBehaviour
{
    private Animator animator;

    private int walking;
    private int jumping;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        walking = Animator.StringToHash(AnimationTags.WALKING);
        jumping = Animator.StringToHash(AnimationTags.JUMPING);
    }

    public void Walk(bool move)
    {
        SetAnimationBool(move, walking);
    }

    private void SetAnimationBool(bool state, int hash)
    {
        bool isActive = animator.GetBool(hash);

        if(!isActive && state) animator.SetBool(hash, state);
        else if(isActive && !state) animator.SetBool(hash, state);
    }
}
