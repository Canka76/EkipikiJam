using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject objectToSpawn; // The object to spawn
    public Transform pointA; // First boundary point
    public Transform pointB; // Second boundary point
    public float spawnInterval = 2.0f; // Time interval between spawns
    public int spawnCount = 10; // Total number of objects to spawn

    private int spawnedObjects = 0; // Counter to keep track of spawned objects

    void Start()
    {
        // Start the spawning loop
        StartCoroutine(SpawnObjects());
    }

    private System.Collections.IEnumerator SpawnObjects()
    {
        while (spawnedObjects < spawnCount)
        {
            Spawn();
            spawnedObjects++;
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void Spawn()
    {
        if (objectToSpawn == null || pointA == null || pointB == null)
        {
            Debug.LogError("Spawner is missing references!");
            return;
        }

        // Calculate a random position between the two points
        float randomX = Random.Range(pointA.position.x, pointB.position.x);
        float randomY = Random.Range(pointA.position.y, pointB.position.y);
        float randomZ = Random.Range(pointA.position.z, pointB.position.z);
        Vector3 spawnPosition = new Vector3(randomX, randomY, randomZ);

        // Instantiate the object at the calculated position
        Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
    }
}