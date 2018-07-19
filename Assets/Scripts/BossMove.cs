using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class BossMove : MonoBehaviour {

    public float speed = 2;
    public float health = 100;

    [SerializeField] GameObject bossLazer;
    [SerializeField] AudioClip die;
    [SerializeField] GameObject destroyeffect;

    LevelManager levelManager;

    bool damage;

    string win = "Win";

    float distancetoCamera;

    public int ScoreValue = 100;
    ScoreKeeper scoreKeeper;

    float bossWidth, bossHight;

    public float padding = 1;

    float leftBoundary, rightBoundary, upBoundary, downBoundary;

    int Hdirection = 1;
    int Vdirection = 1;

    public float countdown = 0.1f;
    public float shootRate = 1f;
   

    // Use this for initialization
    void Start () {
        Camera camera = Camera.main;
        distancetoCamera = transform.position.z - camera.transform.position.z;
        leftBoundary = camera.ViewportToWorldPoint(new Vector3(0, 0, distancetoCamera)).x + padding;
        rightBoundary = camera.ViewportToWorldPoint(new Vector3(1, 1, distancetoCamera)).x - padding;
        upBoundary = camera.ViewportToWorldPoint(new Vector3(0, 0, distancetoCamera)).y + padding;
        downBoundary = camera.ViewportToWorldPoint(new Vector3(1, 1, distancetoCamera)).y - padding;

        bossWidth = GetComponent<SpriteRenderer>().bounds.size.x;
        bossHight = GetComponent<SpriteRenderer>().bounds.size.y;


    }
	
	// Update is called once per frame
	void Update () {
        moveBoundary();
        countdown += Time.deltaTime;
        if (countdown>=shootRate)
        {
            Fire();
        }

        transform.position += new Vector3(Hdirection * speed * Time.deltaTime, Vdirection * speed * Time.deltaTime, 0);

	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Projectile bullet = collision.gameObject.GetComponent<Projectile>();
        scoreKeeper = GameObject.Find("ScoreKeeper").GetComponent<ScoreKeeper>();
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        
        if (bullet != null)
        {
            health -= bullet.ApplyDamage();
            damage = true;
            bullet.Hit();
            if (health <= 0)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                scoreKeeper.Score(ScoreValue);
                AudioSource.PlayClipAtPoint(die, transform.position);
                Destroy(gameObject);
                Destroy(player);
                Instantiate(destroyeffect, transform.position, Quaternion.identity);
                levelManager.LoadLevel(win);
            }
        }
    }

    void Fire()
    {
        GameObject bullet = Instantiate(bossLazer, transform.position, Quaternion.identity) as GameObject;
        GetComponent<AudioSource>().Play();
        countdown = 0;
    }

    void moveBoundary()
    {
        float formationRightEdge = transform.position.x + 0.5f * bossWidth;
        float formationLeftEdge = transform.position.x - 0.5f * bossWidth;
        float formationUpEdge = transform.position.y - 0.5f * bossHight;
        float formationDownEdge = transform.position.y + 0.5f * bossHight;

        if (formationLeftEdge < leftBoundary)
        {
            Hdirection = 1;
        }

        if (formationRightEdge > rightBoundary)
        {
            Hdirection = -1;
        }

        if (formationUpEdge < upBoundary)
        {
            Vdirection = 1;
        }

        if (formationDownEdge > downBoundary)
        {
            Vdirection = -1;
        }
    }

  
}
