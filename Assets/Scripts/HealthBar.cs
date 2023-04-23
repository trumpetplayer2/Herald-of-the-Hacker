using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Health health;
    private Image barImage;
    private void Awake()
    {
        barImage = transform.GetComponent<Image>();

        health = new Health();
    }

    private void Update()
    {
        health.Update();
        barImage.fillAmount = health.GetHealthNormalized();
        if(health.GetHealthNormalized() <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        //Death effects

        //Bring up Death menu
        GameManager.instance.Death();
    }

    public Health GetHealth()
    {
        return health;
    }

    public void Damage(float dmg)
    {
        health.Hurt(dmg);
    }
}

public class Health
{
    public const int HEALTH_MAX = 10;
    private float healthAmount;
    private float regenRate;

    public Health()
    {
        healthAmount = HEALTH_MAX;
        regenRate = 0.01f;
    }

    public void Update()
    {
        healthAmount += regenRate * Time.deltaTime;
        healthAmount = Mathf.Clamp(healthAmount, 0f, HEALTH_MAX);
    }

    public void Hurt(float amount)
    {
        healthAmount -= amount;
    }

    public float GetHealthNormalized()
    {
        return healthAmount / HEALTH_MAX;
    }
}
