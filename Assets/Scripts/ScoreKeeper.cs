using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {
    [HideInInspector]
    public   int score = 0;
    public float NL01score = 60f;
    [SerializeField] Sprite bossbackground;
    [SerializeField] Text Timer;
    [SerializeField] Text timeRemain;

    string seconds;

    public float bosstime = 100f;
    float timer = 0;

    static ScoreKeeper instance = null;

    PlayerMovement player;
    EnemySpawner enemySpawn;

    private void Awake()
    {
        if (instance != null&&instance !=this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            GameObject.DontDestroyOnLoad(gameObject);
        }
    }

    // Use this for initialization
    void Start () {
        score = 0;
	}

    private void Update()
    {
        timer += Time.deltaTime;

        if ( SceneManager.GetActiveScene().name == "Level01")
            {
            seconds = (timer % 60).ToString("00");
            Timer.text = seconds;
            if (timer >= NL01score)
            {
                levelWin();
            }

        }
        if (  SceneManager.GetActiveScene().name == "Level02")
        {
            seconds = (timer % 100).ToString("000");
            Timer.text = seconds;
            if (timer >= bosstime)
            {
                level02Boss();
            }

        }
    }

    public void Score(int points)
    {
        score += points;
       
    }

    void levelWin()
    {
        player = GameObject.Find("playerShip1_red").GetComponent<PlayerMovement>();
        enemySpawn = GameObject.FindGameObjectWithTag("SpawnPoint").GetComponent<EnemySpawner>();
        enemySpawn.KeepSpawn();
        player.OutControl();
        timer = 0;
        timeRemain.text = "/" + bosstime;
    }

    void level02Boss()
    {
        Bossinstantiate bossinst = GameObject.Find("BossInstantiate").GetComponent<Bossinstantiate>();
        Level02ESControl enemySpawn = GameObject.FindGameObjectWithTag("SpawnPoint").GetComponent<Level02ESControl>();
        GameObject.Find("Level_2_background").GetComponent<SpriteRenderer>().sprite = bossbackground;
        enemySpawn.KeepSpawn();
        bossinst.instantiateBoss();
    }

    public void initial()
    {
        timer = 0;
        seconds = (timer % 60).ToString("00");
        Timer.text = seconds;
        timeRemain.text = "/" + NL01score;
    }
   }
