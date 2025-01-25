using UnityEngine;

public class ShootFireball : MonoBehaviour
{
    public GameObject bulletPrefab; 
    public Transform[] firePoints; 
    public float fireRate = 1f; 
    private float bulletSpeed = 0f;
    public float bulletSpeedMax = 10f;
    public float bulletSpeedMin = 5f;

    private float nextFireTime = 0f; 

    void Update()
    {
        
        if (Time.time >= nextFireTime)
            {
                Fire();
                nextFireTime = Time.time + 1f / fireRate;
   
            }
    }

    void Fire()
    {
        bulletSpeed = Random.Range(bulletSpeedMin,bulletSpeedMax);
        int firePointsIndex = Random.Range(0, firePoints.Length);
        GameObject bullet = Instantiate(bulletPrefab, firePoints[firePointsIndex].position, firePoints[firePointsIndex].rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.linearVelocity = firePoints[firePointsIndex].forward * bulletSpeed;
    }
}
