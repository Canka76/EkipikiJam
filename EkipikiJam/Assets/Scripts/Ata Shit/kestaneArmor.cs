using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kestaneArmor : MonoBehaviour
{

    public Animator armorAnimation;
    public GameObject kestane;
    public AI ai;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    IEnumerator disable()
    {
        gameObject.GetComponent<SphereCollider>().enabled = false;
        armorAnimation.SetBool("armor",false);
        kestane.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        kestane.GetComponent<EnemyDamageReceiver>().enabled = true;
        ai.enabled = false;
        yield return new WaitForSeconds(5);
        gameObject.GetComponent<SphereCollider>().enabled = true;
        armorAnimation.SetBool("armor",true);
        kestane.GetComponent<EnemyDamageReceiver>().enabled = false;
        kestane.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
        ai.enabled = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "exBullet")
        {
            StartCoroutine(disable());
        }
    }
}
