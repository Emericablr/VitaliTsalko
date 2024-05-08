using UnityEngine;

public class TreeSpawner : MonoBehaviour
{
    public GameObject treePrefab; 
    public Terrain terrain; 
    public int numberOfTrees = 50; 

    void Start()
    {
        for (int i = 0; i < numberOfTrees; i++)
        {
            float randomX = Random.Range(0, terrain.terrainData.size.x);
            float randomZ = Random.Range(0, terrain.terrainData.size.z);
            Vector3 spawnPosition = new Vector3(randomX, 0, randomZ);

            float terrainHeight = terrain.SampleHeight(spawnPosition);
            spawnPosition.y = terrainHeight;

            Instantiate(treePrefab, spawnPosition, Quaternion.identity);
        }
    }
}
