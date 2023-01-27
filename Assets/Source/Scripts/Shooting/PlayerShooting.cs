using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private List<Weapon> _equipedWeapons;
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private Transform _selfTransform;
    [SerializeField] private float _rotationSpeed = 3;
    [SerializeField] private Weapon _weapon;

    private List<Weapon> _weapons = new List<Weapon>();

    private int _currentWeapon;

    private void Start()
    {
        foreach (var weapon in _equipedWeapons)
        {
            _weapons.Add(Instantiate(weapon));
            _weapons.Last().Disable();
        }
    }

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
        ProcessInput(_inputManager.ShootingDirectionVector3);
    }

    private void ProcessInput(Vector3 direction)
    {
        if (direction.magnitude > Mathf.Epsilon)
        {
            _selfTransform.rotation = Quaternion.Lerp(_selfTransform.rotation, Quaternion.LookRotation(direction), _rotationSpeed * Time.deltaTime);
            
            if(!_weapon.IsShooting && direction.magnitude > .5f)
                _weapon.StartShoot();
            // if(!_equipedWeapons[_currentWeapon].IsShooting)
            //     _equipedWeapons[_currentWeapon].StartShoot();
        }
        // else if(_equipedWeapons[_currentWeapon].IsShooting)
        // {
        //     _equipedWeapons[_currentWeapon].StopShoot();
        // }
        else
        {
            _selfTransform.localRotation = Quaternion.Lerp(_selfTransform.localRotation, Quaternion.identity, _rotationSpeed * Time.deltaTime);

            if (_weapon.IsShooting)
                _weapon.StopShoot();
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

