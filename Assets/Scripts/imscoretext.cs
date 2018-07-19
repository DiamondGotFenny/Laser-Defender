using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class imscoretext : MonoBehaviour {

    ScoreKeeper scoreKper;
    Text mytext;

    // Use this for initialization
    void Start()
    {
        scoreKper = GameObject.Find("ScoreKeeper").GetComponent<ScoreKeeper>();
         mytext = GetComponent<Text>();
       
    }

    // Update is called once per frame
    void Update () {
        mytext.text = scoreKper.score.ToString();
    }
}
