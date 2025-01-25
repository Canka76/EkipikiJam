using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BipolarAI : MonoBehaviour
{
    private ShootAI shootScript;
    private AI ai;
    private int emotionMax = 6;
    private int emotionMin = 1;
    // Start is called before the first frame update
    void Start()
    {  
        shootScript = GetComponent<ShootAI>();
        ai = GetComponent<AI>();
        StartCoroutine(changeEmotion());
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator changeEmotion()
    {
        int emotion = Random.Range(emotionMin,emotionMax);
        if(emotion % 2 == 1) 
        {
            shootScript.enabled = true;
            Debug.Log("Çalıştı");
            ai.distanceFollow = 9;
        }  

        else
        {
            shootScript.enabled = false;
            Debug.Log("Çalışmadı");
            ai.distanceFollow = 1;
        }
        yield return new WaitForSeconds(2);
        StartCoroutine(changeEmotion());
    }
}
