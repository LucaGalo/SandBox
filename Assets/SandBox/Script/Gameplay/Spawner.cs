using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] Spawnable[] spawnPrefabs;
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] int maxInstances;

    /// <summary>
    /// Spawns per minute
    /// </summary>
    [SerializeField] int spawnedAtStart;
    [SerializeField] int spawnRate;

    Queue<Spawnable> pool = new();
    List<Transform> freeSpawnPoints;
    Dictionary<Spawnable, Transform> instanceSpawnerPair = new();

    private void Awake()
    {
        for (int i = 0; i < maxInstances; i++)
        {
            int prefabIndex = Random.Range(0, spawnPrefabs.Length);
            var instance = Instantiate(spawnPrefabs[prefabIndex]);
            instance.Despawn();
            pool.Enqueue(instance);
        }

        freeSpawnPoints = new(spawnPoints);

        for (int i = 0; i < spawnedAtStart; i++)
            Spawn();

        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(60f/spawnRate);
            Spawn();
        }
    }

    public void Spawn()
    {
        if (pool.TryDequeue(out Spawnable instance))
        {
            int spawnIndex = Random.Range(0, freeSpawnPoints.Count);
            var spawnPoint = freeSpawnPoints[spawnIndex];
            freeSpawnPoints.RemoveAt(spawnIndex);

            instanceSpawnerPair.Add(instance, spawnPoint);
            instance.transform.parent = spawnPoint;
            instance.transform.localPosition = Vector3.zero;
            instance.transform.localRotation = Quaternion.identity;
            instance.transform.localScale = Vector3.one;
            instance.Spawn();
        }
    }

    public void Despawn(Spawnable instance)
    {
        instance.Despawn();
        pool.Enqueue(instance);
        var spawnPoint = instanceSpawnerPair[instance];
        freeSpawnPoints.Add(spawnPoint);
        instanceSpawnerPair.Remove(instance);        
    }
}
