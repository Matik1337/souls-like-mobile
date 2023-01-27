using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private float _maxSpeed;

    public float CurrentSpeed { get; private set; }

    private void Update()
    {
        Move(_inputManager.MoveDirectionVector3);
    }

    private void Move(Vector3 direction)
    {
        CurrentSpeed = direction.magnitude * _maxSpeed;
        
        // if(direction.magnitude > float.Epsilon)
        //     transform.LookAt(direction);

        _agent.speed = CurrentSpeed;
        _agent.destination = transform.position + direction;
    }
}
