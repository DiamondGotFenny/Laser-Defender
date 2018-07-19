using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public float damage = 10;
	
    public float ApplyDamage()
    {
        return damage;
    } 

    public void Hit()
    {
        Destroy(gameObject);
    }
    
}
