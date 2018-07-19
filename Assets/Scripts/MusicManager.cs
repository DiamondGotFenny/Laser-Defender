using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour {

    static MusicManager instance = null;

    [SerializeField] AudioClip startMusic;
    [SerializeField] AudioClip gameMusic;
    [SerializeField] AudioClip lostMusic;
    AudioSource muscic;

    // Use this for initialization
    void Start () {
        if (instance !=null&instance !=this)
        {
            Destroy(gameObject);
            print("duplicate musicplayer being destroyed");
        }
        else
        {
            instance = this;
            GameObject.DontDestroyOnLoad(gameObject);
            muscic = GetComponent<AudioSource>();
            muscic.clip = startMusic;
            muscic.loop = true;
            muscic.Play();
        }
	}

    //private void OnLevelWasLoaded(int level)    //the old way;
    //{
    //    muscic.Stop();
    //    if (level==0)
    //    {
    //        muscic.clip = startMusic;
    //    }
    //    if (level==1)
    //    {
    //        muscic.clip = gameMusic;
    //    }
    //    if (level==2)
    //    {
    //        muscic.clip = lostMusic;
    //    }
    //    muscic.loop = true;
    //    muscic.Play();
    //}

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
        muscic.Stop();
        if (scene.name == "menu")
        {
            muscic.clip = startMusic;
        }
        if (scene.name == "Level01"|| scene.name == "Level02")
        {
            muscic.clip = gameMusic;
        }
        if (scene.name == "Lost")
        {
            muscic.clip = lostMusic;
        }
        muscic.loop = true;
        muscic.Play();
    }
}
