using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [Header("Spawn Area")]
    [SerializeField] private float _spawnArea_height = 1f;
    [SerializeField] private float _spawnArea_width = 1f;

    [Header("Settings")]
    [SerializeField] private GameObject _objectPrefab;

    private float maxSpawnRateInSeconds = 5f;

    private void SpawnObject()
    {
        Vector2 minPosition = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 maxPosition = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        GameObject spawnedObject = (GameObject)Instantiate(_objectPrefab);
        Transform localTransform = spawnedObject.transform;

        localTransform.SetParent(transform);

        Vector2 position = transform.position;
        position.x += UnityEngine.Random.Range(-_spawnArea_width, _spawnArea_width);
        position.y += UnityEngine.Random.Range(-_spawnArea_height, _spawnArea_height);

        localTransform.position = position;

        NextObjectSpawn();
    }

    private void NextObjectSpawn()
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
        Gizmos.DrawWireCube(transform.position, new Vector3(_spawnArea_width * 2, _spawnArea_height * 2));
    }
}
