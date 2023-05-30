using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class AgentMovement : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _runSpeed;

    public float CurrentSpeed { get; private set; }

    public void Move(Vector3 direction, bool needRun)
    {
        CurrentSpeed = direction.magnitude * (needRun ? _runSpeed : _maxSpeed);

        _agent.speed = CurrentSpeed;
        _agent.destination = transform.position + direction;
    }
}
