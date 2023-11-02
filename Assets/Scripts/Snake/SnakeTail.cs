using Assets.Scripts;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SnakeTail
{
    private List<Transform> _tail = new List<Transform>();
    private List<Vector3> _lastPositions = new List<Vector3>();
    private GameObjectFactory _factory;

    private int _gap;
    private float _speed;
    private Transform _snakeHeadTransform;

    public SnakeTail(GameObjectFactory factory, int gap, float speed, Transform snakeHeadTransform, int boneCount = 0)
    {
        _factory = factory;
        _gap = gap;
        _speed = speed;
        _snakeHeadTransform = snakeHeadTransform;
        for (int i = 0; i < boneCount; i++)
        {
            AddBone();
        }
    }

    public void AddBone()
    {
        Vector3 position = _tail.Count != 0 ? _tail.Last().position : _snakeHeadTransform.position;
        Transform bone = _factory.SpawnSnakeBone(position).transform;
        _tail.Add(bone);
    }

    public void Move()
    {
        _lastPositions.Insert(0, _snakeHeadTransform.position);
        int index = 0;
        foreach (var bone in _tail)
        {
            Vector3 point = _lastPositions[Mathf.Clamp(index * _gap, 0, _lastPositions.Count - 1)];
            Vector3 movementDirection = point - bone.transform.position;
            bone.transform.position += _speed * Time.deltaTime * movementDirection;
            bone.transform.LookAt(point);

            index++;
        }
    }
}

