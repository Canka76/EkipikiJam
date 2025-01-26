using UnityEngine;

public class kestaneAI : MonoBehaviour
{
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled == true)
        {
            gameObject.GetComponent<EnemyDamageReceiver>().enabled = false;
        }

        else
        {
            gameObject.GetComponent<EnemyDamageReceiver>().enabled = true;
        }
    }
}
