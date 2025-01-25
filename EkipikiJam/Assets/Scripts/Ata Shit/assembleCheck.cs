using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class assembleCheck : MonoBehaviour
{
    public GameObject level2Bubble;
    
    public GameObject spawnPoint;
    
    public int collisionLvl1 = 0;
    public Vector3 newSpawnPoint = new Vector3(0,0,0);
    public Vector3 littleBit = new Vector3(0,1,0);


    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnPoint == null)
        {
            spawnPoint = GameObject.FindWithTag("Spawn Point");
        }

        if(collisionLvl1 == 1)
        {
            StartCoroutine(Wait());
        }
    }

    IEnumerator Wait()
    {
        Spawn();
        yield return new WaitForSeconds(0.1f);
        Destroy(spawnPoint);
        yield return new WaitForSeconds(0.1f);
        Destroy(spawnPoint);
    }

    void Spawn()
    {
        newSpawnPoint = spawnPoint.transform.position + littleBit;
        
        Instantiate(level2Bubble, newSpawnPoint, transform.rotation);
        collisionLvl1 = 0;
    }
}
