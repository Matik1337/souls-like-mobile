using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _runSpeed;

    public float CurrentSpeed { get; private set; }

    private void Update()
    {
        Move(_inputManager.MoveDirectionVector3, _inputManager.ShiftPressed || _inputManager.ShootingDirection == Vector2.zero ? _runSpeed : _maxSpeed);
    }

    private void Move(Vector3 direction, float speed)
    {
        CurrentSpeed = direction.magnitude * speed;

        _agent.speed = CurrentSpeed;
        _agent.destination = transform.position + direction;
    }
}
