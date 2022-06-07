using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    //init health
    public int health;

    //make player receive damage
    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Health = " + health.ToString());
    }
    
}
