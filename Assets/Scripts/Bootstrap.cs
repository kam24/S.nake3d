using Assets.Scripts;
using UnityEngine;
using UnityEngine.Windows;

public class Bootstrap : MonoBehaviour
{
    
    [SerializeField] private GameObjectFactory _factory;
    [SerializeField] private DynamicJoystick _joystick;
    [SerializeField] private PoolService _poolService;
    [SerializeField] private UpperTargetCamera _camera;
    [Header("Game Settings")]
    [SerializeField] private int _foodCount;
    [Header("Snake")]
    [SerializeField] private Transform _snakeSpawn;
    [SerializeField] private int _startBonesCount;
    [SerializeField] private Transform _cameraPoint;
    [Header("Apple")]
    [SerializeField] private Transform _appleSpawn;
    [SerializeField] private LayerMask _appleLayer;
    [SerializeField] private float _sphereRadius;

    private InputRouter _inputRouter;
    private SnakeHead _snakeHead;
    private SphereFoodSpawner _foodSpawner;

    private void Awake()
    {
        _poolService.Init();
        _factory.Init(_poolService);

        GameObject apple = _factory.SpawnApple(_appleSpawn.position);

        _snakeHead = _factory.SpawnSnake(_snakeSpawn.position);
        _snakeHead.Init(_factory, _startBonesCount, apple.transform);

        _camera.Init(_snakeHead.transform, apple.transform);

        _inputRouter = new InputRouter(_joystick, _snakeHead);

        _foodSpawner = new SphereFoodSpawner(apple.transform.position, _sphereRadius, _appleLayer, _factory);
        _foodSpawner.Start(_foodCount);
    }

    private void Update()
    {
        _inputRouter.Update();
    }
}

