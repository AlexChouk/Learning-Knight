using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Health : MonoBehaviour
{
    public Slider healthBar;
    public float health_value;
    public float maxHealth = 50;

    void Start()
    {
        healthBar.value = maxHealth;
    }

    public void DamageLifeHero(float damage)
    {
        health_value -= damage;
        healthBar.value = health_value;   
    }

    public void GainLifeHero(float gain)
    {
        if(health_value != maxHealth)
        {
            if((health_value + gain) >= maxHealth)
            {
                health_value = maxHealth;
            } else 
            {
                health_value += gain;
            }
        }
        healthBar.value = health_value;
    }
}
