using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthEnnemy : MonoBehaviour
{
    //public Slider healthBarEnnemy;
    public float healthEnnemy_value;
    public float maxHealthEnnemy;
    
    void Start()
    {
        healthEnnemy_value = maxHealthEnnemy;
    }

    public void takeDamage(float damage)
    {
        healthEnnemy_value -= damage;
        //healthBarEnnemy.value = healthEnnemy_value;
    }
     
    public bool ennemyDead()
    {
        if(healthEnnemy_value <= 0)
        {
            return true;
        } else
        {
            return false;
        }
    }

}
