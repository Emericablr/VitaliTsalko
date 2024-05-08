using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject prefabToSpawn; 
    public Terrain terrain; 
    public float spawnRadius; 
    public float minSpawnInterval;
    public float maxSpawnInterval; 

    private float nextSpawnTime; 

    void Start()
    {
        nextSpawnTime = Time.time + Random.Range(minSpawnInterval, maxSpawnInterval);
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            Vector3 playerPos = transform.position;
            float posX = playerPos.x + Random.Range(-spawnRadius, spawnRadius);
            float posZ = playerPos.z + Random.Range(-spawnRadius, spawnRadius);
            float posY = terrain.SampleHeight(new Vector3(posX, 0, posZ));

            Vector3 spawnPos = new Vector3(posX, posY, posZ);

            Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);

            nextSpawnTime = Time.time + Random.Range(minSpawnInterval, maxSpawnInterval);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}
