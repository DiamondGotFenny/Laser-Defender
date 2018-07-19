using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class EnemyControll : MonoBehaviour {
    public float health = 10;
    public float projectileSpeed = 5;
    // public float projectileRate = 5f;
    public float shotpersencond = 0.5f;
    public int ScoreValue=10;
    ScoreKeeper scoreKeeper;

    [SerializeField] GameObject enemylaserPrefab;
    [SerializeField] AudioClip die;
    [SerializeField] GameObject dieEffect;
    [SerializeField] GameObject diePieces;

	// Use this for initialization
	void Start () {
        // InvokeRepeating("Fire", 0.0001f, projectileRate * Time.deltaTime);  //method 1 to do it. 
        
	}
	
	// Update is called once per frame
	void Update () {
        float probility = shotpersencond * Time.deltaTime;
        if (Random.value<probility)
        {
            Fire();
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Projectile bullet = collision.gameObject.GetComponent<Projectile>();
        scoreKeeper = GameObject.Find("ScoreKeeper").GetComponent<ScoreKeeper>();
        if ( bullet !=null)
        {
            health -= bullet.ApplyDamage();
            bullet.Hit();
            if (health<=0)
            {
                scoreKeeper.Score(ScoreValue);
                AudioSource.PlayClipAtPoint(die, transform.position);
                Instantiate(dieEffect, transform.position, Quaternion.identity);
               GameObject piecesclone= Instantiate(diePieces, transform.position, Quaternion.identity) as GameObject;
                Destroy(gameObject);
                Destroy(piecesclone, 1f);
            }
        }
        if (collision.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(die, transform.position);
            Destroy(gameObject);
        }
    }

    void Fire()
    {
        GameObject bullet = Instantiate(enemylaserPrefab, transform.position, Quaternion.identity) as GameObject;
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -projectileSpeed, 0);
        GetComponent<AudioSource>().Play();
    }
}
