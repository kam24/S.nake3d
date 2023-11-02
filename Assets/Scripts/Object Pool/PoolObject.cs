using System;
using UnityEngine;

namespace ObjectPool
{
    public class PoolObject : MonoBehaviour
    {
        private GameObjectPool _pool;

        public void Set(GameObjectPool pool)
        {
            if (_pool == null)
                _pool = pool;
            else
                throw new InvalidOperationException();
        }

        public void ReturnToPool()
        {
            _pool.Return(this);
        }
    }
}