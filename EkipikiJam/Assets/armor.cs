using UnityEngine;

public class armor : MonoBehaviour
{
    void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Bullet")
            {
                Destroy(collision.gameObject);
                Debug.Log("Mermi");
            }
        
    }
}
