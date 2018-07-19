using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NLShradder : MonoBehaviour {

    LevelManager levelManager;

    private void Start()
    {
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            levelManager.LoadNextLevel();
        }
    }
}
