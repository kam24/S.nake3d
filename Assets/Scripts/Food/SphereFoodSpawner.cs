using Assets.Scripts;
using System;
using UnityEngine;

public class SphereFoodSpawner
{
    private Vector3 _center;
    private float _radius;
    private LayerMask _sphereMask;
    private GameObjectFactory _factory;

    public SphereFoodSpawner(Vector3 center, float radius, LayerMask sphereMask, GameObjectFactory factory)
    {
        _center = center;
        _radius = radius;
        _sphereMask = sphereMask;
        _factory = factory;
    }

    public void Start(int foodCount)
    {
        for (int i = 0; i < foodCount; i++)
        {
            SpawnFoodOnSphereMesh();
        }
    }

    private void SpawnFoodOnSphereMesh()
    {
        Vector3 random = Utilities.GetRandomDirection();
        if (TryRaycastFromSpherePointToCenter(_center, random, out RaycastHit hit))
        {
            Food newFood = _factory.SpawnFood(hit.point);
            newFood.Collected += OnFoodCollected;
        }
        else
        {
            throw new InvalidOperationException();
        }
    }

    private void OnFoodCollected(Food food)
    {
        food.Collected -= OnFoodCollected;
        food.ReturnToPool();
        SpawnFoodOnSphereMesh();
    }

    private bool TryRaycastFromSpherePointToCenter(Vector3 center, Vector3 direction, out RaycastHit hit)
    {
        Vector3 spherePoint = direction * _radius;
        return Physics.Raycast(spherePoint, center - spherePoint, out hit, Mathf.Infinity, _sphereMask);
    }
}

