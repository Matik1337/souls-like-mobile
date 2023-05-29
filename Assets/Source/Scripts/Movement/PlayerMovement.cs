using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [FormerlySerializedAs("mobileInputManager")] [FormerlySerializedAs("_inputManager")] [SerializeField] private InputManager inputManager;
    [SerializeField] private float _maxSpeed;

    public float CurrentSpeed { get; private set; }

    private void Update()
    {
        Move(inputManager.MoveDirectionVector3);
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
