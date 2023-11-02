using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(Camera))]
public class UpperTargetCamera : Initiable
{
    [SerializeField] private int _distance;
    private Transform _target;
    private Transform _sphereCenter;

    public void Init(Transform target, Transform sphereCenter)
    {
        _target = target;
        _sphereCenter = sphereCenter;

        Init();
    }

    private void LateUpdate()
    {
        FollowTarget(); 
    }

    private void FollowTarget()
    {
        transform.position = _target.position - transform.forward * _distance;
        var gravityDirection = (transform.position - _sphereCenter.position).normalized;
        var upDirection = -transform.forward;
        Quaternion targetRotation = Quaternion.FromToRotation(upDirection, gravityDirection) * transform.rotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 1);
    }
}

