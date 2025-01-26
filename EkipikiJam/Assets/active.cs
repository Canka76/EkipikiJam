using UnityEngine;

public class active : MonoBehaviour
{
    public GameObject spawner1;
    public GameObject spawner2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collision)
    {
        spawner1.SetActive(true);
        spawner2.SetActive(true);

    }
}
