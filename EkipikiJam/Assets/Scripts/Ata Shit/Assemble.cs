using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assemble : MonoBehaviour
{
    public assembleCheck checkScript;
    
    public Vector3 spawnPoint;

    public GameObject spawner;


    void Start()
    {
         if (checkScript == null)
        {
            GameObject checkObject = GameObject.Find("Assemble Check");
            if (checkObject != null)
            {
                checkScript = checkObject.GetComponent<assembleCheck>();
            }
        }
    }

    void Update()
    {
        spawnPoint = gameObject.transform.position; 
    }

    void OnCollisionEnter(Collision collision)
    {
        
        if(collision.gameObject.tag == "Level1")
        {
            Instantiate(spawner, spawnPoint, transform.rotation);
            checkScript.collisionLvl1 =+ 1;
            Debug.Log("carpti");
            Destroy(gameObject);
        }
    }
}
