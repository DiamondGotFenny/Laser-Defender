using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dontdestroyscore : MonoBehaviour {

    static Dontdestroyscore instance = null;

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

    // Update is called once per frame
    void Update () {
		
	}
}
