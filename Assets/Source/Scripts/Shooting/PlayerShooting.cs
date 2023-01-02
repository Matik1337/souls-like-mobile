using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private List<Weapon> _equipedWeapons;
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private Transform _selfTransform;

    private int _currentWeapon;

    private void OnEnable()
    {
        _inputManager.ChangeWeaponButtonClicked += ChangeWeapon;
    }

    private void OnDisable()
    {
        _inputManager.ChangeWeaponButtonClicked -= ChangeWeapon;
    }

    private void Update()
    {
        ProcessInput(_inputManager.ShootingDirection);
    }

    private void ProcessInput(Vector3 direction)
    {
        if (direction.magnitude > Mathf.Epsilon)
        {
            _selfTransform.LookAt(direction);
            
            if(!_equipedWeapons[_currentWeapon].IsShooting)
                _equipedWeapons[_currentWeapon].StartShoot();
        }
        else if(_equipedWeapons[_currentWeapon].IsShooting)
        {
            _equipedWeapons[_currentWeapon].StopShoot();
            _selfTransform.rotation = Quaternion.identity;
        }
    }

    private void ChangeWeapon()
    {
        _equipedWeapons[_currentWeapon].Disable();
        
        _currentWeapon++;

        if (_currentWeapon >= _equipedWeapons.Count)
            _currentWeapon = 0;
        
        _equipedWeapons[_currentWeapon].Enable();
    }
}

