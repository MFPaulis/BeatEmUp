using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterHealth : MonoBehaviour
{
    [SerializeField] private HealthBar healthBar;
    private HealthSystem healthSystem;

    private void Start()
    {
        healthSystem = new HealthSystem(100);
        healthBar.Setup(healthSystem);
        healthSystem.OnDeath += Die;
    }

    public void Damage(int damageAmount)
    {
        healthSystem.Damage(damageAmount);
    }

    public void Die(object sender, System.EventArgs e)
    {
        SceneManager.LoadScene("GameOver");
    }
}
