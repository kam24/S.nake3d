using Assets.Scripts;
using UnityEngine;
using static UnityEngine.GridBrushBase;

public class SnakeHead : Initiable
{
    [SerializeField] private float _speed = 0.01f;
    [SerializeField] private LayerMask _ground;
    [SerializeField] private int _gap = 10;
    [SerializeField] private float _rotationSpeed = 50;

    private Transform _sphereCenter;
    private Rigidbody _rigidbody;
    private SnakeTail _tail;

    private float _gravity = -10;

    public void Init(GameObjectFactory factory, int _bonesCount, Transform sphereCenter)
    {
        _tail = new SnakeTail(factory, _gap, _speed, transform, _bonesCount);
        _rigidbody = GetComponent<Rigidbody>();
        _sphereCenter = sphereCenter;

        Init();
    }

    public void Rotate(Vector2 inputDirection)
    {
        var movementDirection = new Vector3(inputDirection.x, 0, inputDirection.y).normalized;
        if (movementDirection != Vector3.zero)
        {
            var relative = (transform.position + movementDirection) - transform.position;
            var targetRotation = Quaternion.LookRotation(relative, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 180 * Time.deltaTime);
        }
    }

    private void Update()
    { 
        MoveForward();
        PullToSphere();
        _tail.Move();
        Debug.DrawLine(transform.position, transform.position + transform.forward * 5);
    }

    private void MoveForward()
    {
        transform.Translate(_speed * Time.deltaTime * Vector3.forward);
    }

    private void PullToSphere()
    {
        Vector3 gravityDirection = (transform.position - _sphereCenter.position).normalized;
        Vector3 upDirection = transform.up;
        _rigidbody.AddForce(_gravity * upDirection);
        Quaternion targetRotation = Quaternion.FromToRotation(upDirection, gravityDirection) * transform.rotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Food>(out _))
        {
            _tail.AddBone();
        }
    }
}
