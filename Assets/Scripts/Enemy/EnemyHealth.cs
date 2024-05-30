using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private HealthBar healthBar;
    private HealthSystem healthSystem;

    private void Start()
    {
        healthSystem = new HealthSystem(100);
        healthBar.Setup(healthSystem);
        gameObject.GetComponent<Enemy>().Setup(healthSystem);
    }

    public void Damage(int damageAmount)
    {
        healthSystem.Damage(damageAmount);
    }
}
