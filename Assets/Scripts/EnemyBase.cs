using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public float health = 5;
    public float damage;
    protected bool isDead = false;

    public void hit(float incomingDamage)
    {
        health -= incomingDamage;
        if(health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        //Toggle dead
        isDead = true;
        //Animations or effects go here

        //Add any timing needed
        Destroy(this.gameObject);
    }
}
