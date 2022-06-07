using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour
{   

    //Function to get Player stat "Health" and then Subtract Health by amount given
    public void SendDamage (int dam)
    {
        PlayerHealth playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        playerStats.TakeDamage(dam);
    }
}
