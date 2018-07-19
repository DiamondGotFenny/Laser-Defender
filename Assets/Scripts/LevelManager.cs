using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class LevelManager : MonoBehaviour {

    static LevelManager instance=null;

    private void Awake()
    {
        if (instance !=null&&instance !=this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            GameObject.DontDestroyOnLoad(gameObject);
        }
    }

    public void LoadLevel(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void QuitRequest()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Reset()
    {
       ScoreKeeper scoreKper = GameObject.Find("ScoreKeeper").GetComponent<ScoreKeeper>();
        scoreKper.score = 0;
        scoreKper.initial();
    }
}
