using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {
    public float Health = 100;
    static PlayerHealth instance = null;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            GameObject.DontDestroyOnLoad(gameObject);
        }
        
    }

    private void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyProjectile enemybullet = collision.GetComponent<EnemyProjectile>();
        LevelManager levelManager= GameObject.Find("LevelManager").GetComponent<LevelManager>();

        if (enemybullet)
        {
            Health -= enemybullet.ApplyDamage();
            enemybullet.Hit();
            if (Health<=0)
            {            
                Destroy(gameObject);
                levelManager.LoadLevel("Lost");
            }
        }
        if ( collision.tag == "Enemy"||collision.name==("Boss_Octopus"))
        {
            Health = 0;
            Destroy(gameObject);
            levelManager.LoadLevel("Lost");
        }
    }
}
