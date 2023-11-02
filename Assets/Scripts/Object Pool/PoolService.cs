using ObjectPool;
using UnityEngine;

public class PoolService : Initiable
{
    [SerializeField] private GameObjectPool _foodPool;
    [SerializeField] private GameObjectPool _snakeBonesPool;

    public GameObjectPool FoodPool => _foodPool;
    public GameObjectPool SnakeBonesPool => _snakeBonesPool;

    public override void Init()
    {
        FoodPool.Init();
        SnakeBonesPool.Init();

        base.Init();
    }
}

