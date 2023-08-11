using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnerScript : MonoBehaviour
{
    public Transform playerTransform;
    public GameObject birdPrefab;
    public GameObject cloudPrefab;
    public GameObject finishPrefab;

    public int lastMaxVal;

    public float chunkWidth;

    public int numOfBirds;
    public int numOfClouds;

    public int numOfChunks;

    public float maxHeight;
    public float minHeight;

    public float birdTeleportPos;

    public float cloudSpawnHeight;
    public float cloudSpawnScattering;

    public float spawnChance;
    public float spawnM;
    public float spawnRandVal;

    void Start()
    {
        SpawnBirds();
        SpawnClouds(0);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(Vector3.up * maxHeight + Vector3.left * 2f, Vector3.up * maxHeight + Vector3.right * 2f);
        Gizmos.DrawLine(Vector3.up * minHeight + Vector3.left * 2f, Vector3.up * minHeight + Vector3.right * 2f);
        Gizmos.DrawLine(Vector3.down, Vector3.up);
        Gizmos.DrawLine(Vector2.right * chunkWidth + Vector2.down, Vector2.right * chunkWidth + Vector2.up);

        Gizmos.DrawLine(new Vector3(-1f, cloudSpawnHeight), new Vector3(1f, cloudSpawnHeight));
        Gizmos.DrawLine(new Vector3(-1f, cloudSpawnHeight - cloudSpawnScattering), new Vector3(1f, cloudSpawnHeight - cloudSpawnScattering));
        Gizmos.DrawLine(new Vector3(-1f, cloudSpawnHeight + cloudSpawnScattering), new Vector3(1f, cloudSpawnHeight + cloudSpawnScattering));

        Gizmos.DrawLine(new Vector3(birdTeleportPos, -1f), new Vector3(birdTeleportPos, 1f));
    }

    void Update()
    {
        /*int val = Mathf.FloorToInt(playerTransform.position.x / chunkWidth);
        //Debug.Log(playerTransform.position.ToString() + "  " + val.ToString());
        if (val > lastMaxVal) 
        {
            SpawnBirds(val + 1);
            SpawnClouds(val + 1);
            lastMaxVal = val;
        }*/
    }

    private void SpawnBirds() 
    {
        float height = maxHeight - minHeight;
        for (float i = 0; i < spawnM * chunkWidth * numOfChunks; i += spawnM)
        {
            for (float j = 0; j < spawnM * height; j += spawnM)
            {
                float p = Mathf.PerlinNoise(i, j);
                if(p > spawnChance)
                    SpawnBird(new Vector2(i / spawnM + Random.Range(-spawnRandVal, spawnRandVal), j / spawnM + minHeight + Random.Range(-spawnRandVal, spawnRandVal)));
            }
        }
    }

    private void SpawnBird(Vector2 pos) 
    {
        GameObject bird = Instantiate(birdPrefab, pos, Quaternion.identity, transform);
        bird.GetComponent<BirdScript>().spawner = this;
    }

    private void SpawnClouds(int x) 
    {
        for (int i = 0; i < numOfClouds; i++) 
        {
            Vector2 pos = Vector2.right * Random.Range(x * chunkWidth, (x + 1) * chunkWidth) + Vector2.up * Random.Range(cloudSpawnHeight - cloudSpawnScattering, cloudSpawnHeight + cloudSpawnScattering);
            Instantiate(cloudPrefab, pos, Quaternion.identity, transform);
        }
    }

    public GameObject SpawnFinish() 
    {
        Vector2 pos = new Vector2(numOfChunks * chunkWidth, -3.6f);
        return Instantiate(finishPrefab, pos, Quaternion.identity, transform);
    }
}
