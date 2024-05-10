using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    [SerializeField] private HealthBar healthBar;
    private HealthSystem healthSystem;

    private void Start()
    {
        healthSystem = new HealthSystem(100);
        healthBar.Setup(healthSystem);

        healthSystem.Damage(10);
    }

}
