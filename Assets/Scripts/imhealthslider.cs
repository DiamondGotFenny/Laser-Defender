using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class imhealthslider : MonoBehaviour {

    PlayerHealth playerHealth;
    Slider healthbar;

    // Use this for initialization
    void Start()
    {
        playerHealth = GameObject.Find("playerShip1_red").GetComponent<PlayerHealth>();
        healthbar = GetComponent<Slider>();

    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth != null)
        {
            healthbar.value = playerHealth.Health;
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
        if (scene.name == "Level01")
        {
            Start();
        }
    }
}
