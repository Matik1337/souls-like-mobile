using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class InputManager : MonoBehaviour
{
    public bool NeedRun { get; protected set; }
    public bool NeedShoot { get; protected set; }
    
    public event UnityAction ChangeWeaponButtonClicked;
    public event UnityAction AbilityButtonClicked;
    public event UnityAction InteractionButtonClicked;
    
    public abstract Vector3 GetMoveDirection();
    public abstract Vector3 GetShootDirection();

    protected void AbilityClick()
    {
        AbilityButtonClicked?.Invoke();
    }

    protected void WeaponClick()
    {
        ChangeWeaponButtonClicked?.Invoke();
    }

    protected void Interact()
    {
        InteractionButtonClicked?.Invoke();
    }
    
    protected Vector3 ToVector3(Vector2 vector) => new (vector.x, 0, vector.y);
    
    protected Vector2 RotateDirection(Vector2 direction, float angle)
    {
        if (direction == Vector2.zero)
            return direction;
        
        float angleRad = -angle * 2;
        Vector2 result = direction;

        result.x = direction.x * Mathf.Cos(angleRad) - direction.y * Mathf.Sin(angleRad);
        result.y = direction.x * Mathf.Sin(angleRad) + direction.y * Mathf.Cos(angleRad);

        if (result.magnitude > 1)
            result.Scale(new (1 / result.magnitude, 1/ result.magnitude));

        return result;
    }
}
