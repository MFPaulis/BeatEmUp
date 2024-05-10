using System;

public class HealthSystem
{
    public event EventHandler OnHealthChanged;

    private int maxHealth;
    private int health {  get; set; }

    public HealthSystem(int maxHealth)
    {
        this.maxHealth = maxHealth;
        health = maxHealth;
    }

    public void Damage(int damageAmount)
    {
        health -= damageAmount;
        if(health < 0) health = 0;

        OnHealthChanged?.Invoke(this, EventArgs.Empty);
    }

    public void Heal(int healAmount)
    {
        health -= healAmount;
        if (health > maxHealth) health = maxHealth;

        OnHealthChanged?.Invoke(this, EventArgs.Empty);
    }

    public float GetHealthPercentage()
    {
        return (float)health / maxHealth;
    }

}
