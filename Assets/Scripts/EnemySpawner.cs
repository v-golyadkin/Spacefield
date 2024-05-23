using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn Area")]
    [SerializeField] private float spawnArea_height = 1f;
    [SerializeField] private float spawnArea_width = 1f;

    [Header("Settings")]
    [SerializeField] private GameObject enemyPrefab;

    private float maxSpawnRateInSeconds = 5f;

    private void Start()
    {

    }

    private void Update()
    {
        
    }

    private void SpawnObject()
    {
        Vector2 minPosition = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 maxPosition = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        GameObject Enemy = (GameObject)Instantiate(enemyPrefab);
        Transform localTransform = Enemy.transform;

        localTransform.SetParent(transform);

        Vector2 position = transform.position;
        position.x += UnityEngine.Random.Range(-spawnArea_width, spawnArea_width);
        position.y += UnityEngine.Random.Range(-spawnArea_height, spawnArea_height);

        localTransform.position = position;

        NextEnemySpawn();
    }

    private void NextEnemySpawn()
    {
        float spawnInNSeconds;

        if(maxSpawnRateInSeconds > 1f)
        {
            spawnInNSeconds = Random.Range(1f, maxSpawnRateInSeconds);
        }
        else
        {
            spawnInNSeconds = 1f;
        }

        Invoke("SpawnObject", spawnInNSeconds);
    }

    private void IncreaseSpawnRate()
    {
        if(maxSpawnRateInSeconds > 1f) 
        {
            maxSpawnRateInSeconds--;
        }

        if(maxSpawnRateInSeconds == 0f)
        {
            CancelInvoke("IncreaseSpawnRate");
        }
    }

    public void StartObjectSpawn()
    {
        Invoke("SpawnObject", maxSpawnRateInSeconds);

        InvokeRepeating("IncreaseSpawnRate", 0f, 30f);
    }

    public void StopObjectSpawn()
    {
        CancelInvoke("SpawnObject");
        CancelInvoke("IncreaseSpawnRate");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(transform.position, new Vector3(spawnArea_width * 2, spawnArea_height * 2));
    }
}
