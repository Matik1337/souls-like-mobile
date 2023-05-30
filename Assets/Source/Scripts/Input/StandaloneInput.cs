using System;
using UnityEngine;

public class StandaloneInput : InputManager
{
    [SerializeField] private LayerMask _groundLayer;

    private Camera _camera;
    
    public Vector3 MouseHitPoint { get; private set; }

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
            AbilityClick();
        if(Input.GetKeyDown(KeyCode.F))
            WeaponClick();
        if(Input.GetKeyDown(KeyCode.E))
            Interact();
        
        NeedShoot = Input.GetMouseButton(0);
        NeedRun = Input.GetKey(KeyCode.LeftShift) && !NeedShoot;
    }

    public override Vector3 GetMoveDirection()
    {
        Vector2 direction = new (Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        
        return ToVector3(RotateDirection(direction, _camera.transform.rotation.y));
    }

    public override Vector3 GetShootDirection()
    {
        return ToVector3(GetMouseDeviation());
    }
    
    private Vector2 GetMouseDeviation()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (!NeedRun && Physics.Raycast(ray, out RaycastHit hit, 100, _groundLayer))
        {
            MouseHitPoint = hit.point;
            
            Vector3 direction = MouseHitPoint - transform.position;
            
            return new Vector2(direction.x, direction.z);
        }
        
        return Vector2.zero;
    }
}
