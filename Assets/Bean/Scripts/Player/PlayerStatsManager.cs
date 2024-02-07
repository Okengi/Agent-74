using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsManager : MonoBehaviour
{
    private float Health;
    public float MaxHealth;

    private float Shield;
    public float MaxShield;


    public void TakeDamage(float damage)
    {
        Health -= damage;
    }
}
