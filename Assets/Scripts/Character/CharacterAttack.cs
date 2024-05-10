using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ComboState
{
    NONE,
    PUNCH_1,
    PUNCH_2,
    KICK_1,
    KICK_2,
}

public class CharacterAttack : MonoBehaviour
{
    private CharacterAnimations animations;
    private ComboState currentComboState;

    private void Awake()
    {
        animations = GetComponentInChildren<CharacterAnimations>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
