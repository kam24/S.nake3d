using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ObjectPool
{
    public class GameObjectPool : Initiable
    {
        [SerializeField] protected PoolObject _prefab;
        [SerializeField] private int _capacity;

        protected List<PoolObject> _objects;

        public override void Init()
        {
            _objects = new List<PoolObject>(_capacity);
            for (int i = 0; i < _capacity; i++)
                AddObject();
            base.Init();
        }

        public PoolObject Get(Vector3 position)
        {
            PoolObject go = _objects.FirstOrDefault(go => go.gameObject.activeInHierarchy == false);

            if (go == null)
                go = AddObject();

            go.transform.position = position;
            go.gameObject.SetActive(true);
            return go;
        }

        public void Return(PoolObject gameObject)
        {
            PoolObject returningObject = _objects.FirstOrDefault(go => go == gameObject);

            if (returningObject != null)
                returningObject.gameObject.SetActive(false);
            else
                throw new InvalidOperationException();
        }

        protected PoolObject AddObject()
        {
            PoolObject prefab = Instantiate(_prefab, transform);
            prefab.gameObject.SetActive(false);
            _objects.Add(prefab);
            prefab.Set(this);

            return prefab;
        }
    }
}

