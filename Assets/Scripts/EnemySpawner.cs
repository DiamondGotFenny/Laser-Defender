using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [SerializeField] GameObject enemyPrefeb;

    public float width;
    public float height;
    public float speed = 2;
    public float padding = 1;
    public float SpawnDelaySeconds = 1f;

    Animator anmit;

    float xMax, xMin, yMax, yMin;
    int Hdirection=1;
    int Vdirection = 1;
    float leftBoundary, rightBoundary,upBoundary, downBoundary;
    float distancetoCamera;

    bool keepspawn = true;

    // Use this for initialization
    void Start () {
        Camera camera = Camera.main;
        distancetoCamera = transform.position.z - camera.transform.position.z;
        leftBoundary = camera.ViewportToWorldPoint(new Vector3(0, 0, distancetoCamera)).x + padding;
        rightBoundary= camera.ViewportToWorldPoint(new Vector3(1, 1, distancetoCamera)).x - padding;
        upBoundary = camera.ViewportToWorldPoint(new Vector3(0, 0, distancetoCamera)).y + padding;
        downBoundary = camera.ViewportToWorldPoint(new Vector3(1, 1, distancetoCamera)).y - padding;

        SpawnEnemies();
	}

    private void OnDrawGizmos()
    {
        xMin = transform.position.x - 0.5f*width;
        xMax=transform.position.x+0.5f * width;
        yMin = transform.position.y - 0.5f * height;
        yMax = transform.position.y + 0.5f * height;

        Gizmos.DrawLine(new Vector3(xMin, yMin, 0), new Vector3(xMin, yMax));
        Gizmos.DrawLine(new Vector3(xMin, yMax, 0), new Vector3(xMax, yMax));
        Gizmos.DrawLine(new Vector3(xMax, yMax, 0), new Vector3(xMax, yMin));
        Gizmos.DrawLine(new Vector3(xMax, yMin, 0), new Vector3(xMin, yMin));
    }

    // Update is called once per frame
    void Update () {
        float formationRightEdge = transform.position.x + 0.5f * width;
        float formationLeftEdge = transform.position.x - 0.5f * width;
        float formationUpEdge = transform.position.y-0.5f * height;
        float formationDownEdge = transform.position.y + 0.5f * height;

        if (formationLeftEdge<leftBoundary)
        {
            Hdirection = 1;
        }

        if (formationRightEdge>rightBoundary)
        {
            Hdirection = -1;
        }

        if (formationUpEdge < upBoundary)
        {
            Vdirection = 1;
        }

        if (formationDownEdge>downBoundary)
        {
            Vdirection = -1;
        }

        transform.position += new Vector3(Hdirection* speed * Time.deltaTime, Vdirection * speed * Time.deltaTime, 0);

        if (AllMemeberAreDead()&&keepspawn==true)
        {
            SpawnUntilFull();
        }

        if (keepspawn==false)
        {
            
            GameObject[] enemyclones = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemyclones)
            {
                anmit = enemy.GetComponent<Animator>();
                anmit.SetTrigger("die");
                Destroy(enemy,0.5f);
            }
        }
    }

    bool AllMemeberAreDead()
    {
        foreach (Transform position in transform)
        {
            if (position.childCount>0)
            {
                return false;
            }
        }
        return true;
    }

    void SpawnEnemies()
    {
        foreach (Transform child in transform)
        {

            GameObject enemyclone = Instantiate(enemyPrefeb, child.transform.position, Quaternion.identity) as GameObject;
            enemyclone.transform.parent = child;
        }
    }

    Transform NextFreePosition()
    {
        foreach (Transform childPosition in transform)
        {
            if (childPosition.childCount<=0)
            {
                return childPosition;
            }
        }
        return null;
    }

    bool FreePositionExists()
    {
        foreach (Transform position in transform)
        {
            if (position.childCount<=0)
            {
                return true;
            }
        }
        return false;
    } 

    void SpawnUntilFull()
    {
        Transform freepos = NextFreePosition();
        if (freepos)
        {
            GameObject enemyclone = Instantiate(enemyPrefeb, freepos.position, Quaternion.identity) as GameObject;
            enemyclone.transform.parent = freepos;
        }     
        if (FreePositionExists())
        {
            Invoke("SpawnUntilFull", SpawnDelaySeconds);
        }
    }

    public bool KeepSpawn()
    {
        return keepspawn = false;      
    }

}
