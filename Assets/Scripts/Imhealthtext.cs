using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Imhealthtext : MonoBehaviour {

    PlayerHealth playerHealth;
    Text mytext;

    // Use this for initialization
    void Start()
    {
        playerHealth = GameObject.Find("playerShip1_red").GetComponent<PlayerHealth>();
        mytext = GetComponent<Text>();

    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth !=null)
        {
            mytext.text = playerHealth.Health.ToString();
        }

    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinshedLoading;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinshedLoading;
    }

    void OnLevelFinshedLoading(Scene scene, LoadSceneMode mode)
    {
        if (scene.name== "Level01")
        {
            Start();
        }
    }
}
