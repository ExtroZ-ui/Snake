using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleSpawner : MonoBehaviour
{

    public GameObject applePrefab;
    public Collider spawnCollider;


    void Start()
    {
        SpawnerApples();
    }

    public void SpawnerApples(){
        Bounds bounds = spawnCollider.bounds;
        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomY = Random.Range(bounds.min.y, bounds.max.y);
        float randomZ = Random.Range(bounds.min.z, bounds.max.z);

        Instantiate(applePrefab, new Vector3(randomX, randomY, randomZ), Quaternion.identity);
    }
}
