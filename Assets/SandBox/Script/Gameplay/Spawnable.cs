using System;
using UnityEngine;

public class Spawnable : MonoBehaviour
{
    public event Action OnSpawn;
    public event Action OnDespawn;

    public void Spawn()
    {
        OnSpawn?.Invoke();
        gameObject.SetActive(true);
    }

    public void Despawn()
    {
        OnDespawn?.Invoke();
        gameObject.SetActive(false);
    }
}
