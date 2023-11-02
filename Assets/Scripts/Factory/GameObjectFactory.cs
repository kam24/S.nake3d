using ObjectPool;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameObjectFactory : Initiable
    {
        [SerializeField] private SnakeHead _snakePrefab;
        [SerializeField] private GameObject _apple;
        private PoolService _poolService;

        public void Init(PoolService poolService)
        {
            _poolService = poolService;
            Init();
        }

        public SnakeHead SpawnSnake(Vector3 position)
        {
            return Instantiate(_snakePrefab, position, Quaternion.identity);
        }

        public Food SpawnFood(Vector3 position)
        {
            return _poolService.FoodPool.Get(position) as Food;
        }

        public PoolObject SpawnSnakeBone(Vector3 position)
        {
            return _poolService.SnakeBonesPool.Get(position);
        }

        public GameObject SpawnApple(Vector3 position)
        {
            return Instantiate(_apple, position, Quaternion.identity);
        }
    }
}
