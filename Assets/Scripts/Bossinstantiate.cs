using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bossinstantiate : MonoBehaviour {

    bool iscreate = false;

    [SerializeField] GameObject bossPrefeb;

	public void instantiateBoss()
    {
        if (!iscreate)
        {
            GameObject boss = Instantiate(bossPrefeb, transform.position, Quaternion.identity);
            iscreate = true;
        }
      
    }
}
