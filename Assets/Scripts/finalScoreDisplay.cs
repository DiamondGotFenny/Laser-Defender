using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class finalScoreDisplay : MonoBehaviour {

    ScoreKeeper scoreKper;

	// Use this for initialization
	void Start () {
        scoreKper = GameObject.Find("ScoreKeeper").GetComponent<ScoreKeeper>();
        Text mytext = GetComponent<Text>();
        mytext.text = scoreKper.score.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
