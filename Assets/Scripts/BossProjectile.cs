using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile : MonoBehaviour {
    public float projectileSpeed = 5;
     Transform target;

    // Use this for initialization
    void Start () {
        GameObject go = GameObject.FindGameObjectWithTag("Player");
        target = go.transform;
    }
	
	// Update is called once per frame
	void Update () {

        float aimtomove = projectileSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, aimtomove);
        Destroy(gameObject, 5f);
	}
}
