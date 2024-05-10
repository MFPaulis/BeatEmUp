using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private HealthSystem healthSystem;
    [SerializeField] private Image bar;

    public void Setup(HealthSystem healthSystem)
    {
        this.healthSystem = healthSystem;
        healthSystem.OnHealthChanged += OnHealthChange;
    }

    private void OnHealthChange(object sender, System.EventArgs e)
    {
        bar.fillAmount = healthSystem.GetHealthPercentage();
    }
}
