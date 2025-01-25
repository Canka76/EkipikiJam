using UnityEngine;

public class ShootAI : MonoBehaviour
{
    public GameObject player; 
    public GameObject bulletPrefab; 
    public Transform firePoint; 
    
    public float detectionRange = 5f; 
    public float fireRate = 1f; 
    public float bulletSpeed = 10f; 
    public float minDetectionRange =5f;

    private float nextFireTime = 0f; 

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= detectionRange && distanceToPlayer >= minDetectionRange)
        {
            LookAtPlayer();

            if (Time.time >= nextFireTime)
            {
                Fire();
                nextFireTime = Time.time + 1f / fireRate;
            }
        }
    }

    void LookAtPlayer()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.linearVelocity = firePoint.forward * bulletSpeed;
    }
}
