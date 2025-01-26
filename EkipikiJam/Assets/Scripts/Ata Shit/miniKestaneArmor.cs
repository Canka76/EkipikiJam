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
    
    void OnTriggerenter(Collider other)
    {
        if(other.gameObject.tag == "exBullet")
        {
            Destroy(gameObject);
        }

        else if(other.gameObject.tag == "Bullet")
            {
                Destroy(other.gameObject);
                Debug.Log("Mermi");
            }
    }
}
