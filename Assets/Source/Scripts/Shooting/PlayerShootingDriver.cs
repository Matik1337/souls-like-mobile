using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerShootingDriver : MonoBehaviour, IShootingDriver
{
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private ShootingComponent _shootingComponent;
    
    private IWeaponAcсess _weaponAccess;

    private void OnEnable()
    {
        _inputManager.ChangeWeaponButtonClicked += OnChangeWeapon;
    }

    private void OnDisable()
    {
        _inputManager.ChangeWeaponButtonClicked -= OnChangeWeapon;
    }

    private void Update()
    {
        _shootingComponent.ProcessInput(GetDirection(), GetShootAbility());
    }

    public Vector3 GetDirection()
    {
        return _inputManager.GetShootDirection();
    }

    public bool GetShootAbility()
    {
        return _inputManager.NeedShoot;
    }

    private void OnChangeWeapon()
    {
        _weaponAccess.ChangeCurrentWeapon();
        _shootingComponent.SetCurrentWeapon(_weaponAccess.GetCurrentWeapon());
    }

    public void SetWeaponAccess(IWeaponAcсess acсess)
    {
        _weaponAccess = acсess;
        _shootingComponent.SetCurrentWeapon(_weaponAccess.GetCurrentWeapon());
    }
}
