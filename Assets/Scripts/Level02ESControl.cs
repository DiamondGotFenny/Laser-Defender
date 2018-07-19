using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level02ESControl : MonoBehaviour {

    [SerializeField] GameObject[] enemy_02prefab;

    public float repeatrate = 4f;
    public float flySpeed = 5;
    float optionenemy;
    int index;
    float counter;

    bool keepspawn = true;

    // Use this for initialization
    void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {
        counter+=Time.deltaTime*Random.value;
        if (counter>repeatrate&&keepspawn==true)
        {
            SpawnEnemy();
            counter = 0;
        }

        if (keepspawn == false)
        {

            GameObject[] enemyclones = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemyclones)
            {
                Destroy(enemy, 0.5f);
            }
        }

    }

    void SpawnEnemy()
    {
        foreach (Transform child in transform)
        {
            index = Random.Range(0, enemy_02prefab.Length);
            GameObject enemyclone = Instantiate(enemy_02prefab[index], child.transform.position, Quaternion.identity) as GameObject;
            enemyclone.transform.parent = child;
            enemyclone.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -flySpeed * Random.value, 0);
        }
    }

    public bool KeepSpawn()
    {
        return keepspawn = false;
    }

}
