using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AppleSpawner : MonoBehaviour
{

    public GameObject UI_timer;
    public GameObject applePrefab;
    public GameObject appleBonusPrefab;
    public Collider spawnCollider;
    public int countGrayApple;


    void Start()
    {
        SpawnerApples();
    }


    public void SpawnerApples()
    {
        SpawnerApplesBonus();

        Bounds bounds = spawnCollider.bounds;
        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomY = Random.Range(bounds.min.y, bounds.max.y);
        float randomZ = Random.Range(bounds.min.z, bounds.max.z);

        Instantiate(applePrefab, new Vector3(randomX, randomY, randomZ), Quaternion.identity);
    }

    public void SpawnerApplesBonus()
    {
        if (countGrayApple == 10)
        {
            UI_timer.SetActive(true);
            Bounds bounds = spawnCollider.bounds;
            float randomX = Random.Range(bounds.min.x, bounds.max.x);
            float randomY = Random.Range(bounds.min.y, bounds.max.y);
            float randomZ = Random.Range(bounds.min.z, bounds.max.z);

            Instantiate(appleBonusPrefab, new Vector3(randomX, randomY, randomZ), Quaternion.identity);
            countGrayApple = 0;
        }

    }

}
