using ObjectPool;
using System;
using UnityEngine;

public class Food : PoolObject
{
    public event Action<Food> Collected;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<SnakeHead>(out _))
        {
            Collected?.Invoke(this);
        }
    }
}

