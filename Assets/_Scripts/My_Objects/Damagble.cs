using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagble : MonoBehaviour
{
    public float Health;

    public void Damage(float damage)
    {
        Health = Health - damage;
        if(Health <= 0 )
        {
            Delete();
        }
        else
            TakeDamage();
    }
    private void Delete()
    {
		Destroy(this.gameObject);
	}
    private void TakeDamage()
    {

    } 

}
