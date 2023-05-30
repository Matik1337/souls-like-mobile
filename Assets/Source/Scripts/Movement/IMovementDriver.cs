using UnityEngine;

public interface IMovementDriver
{
    public Vector3 GetDirection();

    public bool GetRunAbility();
}
