using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class miniKestaneArmor : MonoBehaviour
{
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "exBullet")
        {
            Destroy(gameObject);
        }
    }
}
