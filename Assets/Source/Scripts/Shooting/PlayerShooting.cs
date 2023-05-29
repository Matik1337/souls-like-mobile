using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private Transform _selfTransform;
    [SerializeField] private float _rotationSpeed = 3;
    [SerializeField] private PlayerInventoryHolder _playerInventoryHolder;

    private Weapon _currentWeapon => _playerInventoryHolder.CurrentWeapon;

    private void OnEnable()
    {
        _inputManager.ChangeWeaponButtonClicked += _playerInventoryHolder.ChangeWeapon;
    }

    private void OnDisable()
    {
        _inputManager.ChangeWeaponButtonClicked -= _playerInventoryHolder.ChangeWeapon;
    }

    private void Update()
    {
        ProcessInput(_inputManager.ShootingDirectionVector3, _inputManager.ShootingActive);
    }

    private void ProcessInput(Vector3 direction, bool canShoot)
    {
        if (direction.magnitude > Mathf.Epsilon)
        {
            _selfTransform.rotation = Quaternion.Lerp(_selfTransform.rotation, Quaternion.LookRotation(direction), _rotationSpeed * Time.deltaTime);

            if (canShoot && !_currentWeapon.IsShooting && direction.magnitude > .5f)
                _currentWeapon.StartShoot();
            else if (!canShoot && _currentWeapon.IsShooting)
                _currentWeapon.StopShoot();

        }
        else
        {
            _selfTransform.localRotation = Quaternion.Lerp(_selfTransform.localRotation, Quaternion.identity, _rotationSpeed * Time.deltaTime);

            if (!canShoot && _currentWeapon.IsShooting)
                _currentWeapon.StopShoot();
        }
    }
}

