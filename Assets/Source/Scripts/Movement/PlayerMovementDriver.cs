using UnityEngine;

public class PlayerMovementDriver : MonoBehaviour, IMovementDriver
{
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private AgentMovement _agentMovement;

    private void Update()
    {
        _agentMovement.Move(GetDirection(), GetRunAbility());
    }

    public Vector3 GetDirection()
    {
        return _inputManager.GetMoveDirection();
    }

    public bool GetRunAbility()
    {
        return _inputManager.NeedRun;
    }
}
