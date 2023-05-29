using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerShooting : MonoBehaviour
{
    [FormerlySerializedAs("mobileInputManager")] [FormerlySerializedAs("_inputManager")] [SerializeField] private InputManager inputManager;
    [SerializeField] private Transform _selfTransform;
    [SerializeField] private float _rotationSpeed = 3;
    [SerializeField] private PlayerInventoryHolder _playerInventoryHolder;

    private Weapon _currentWeapon => _playerInventoryHolder.CurrentWeapon;

    private void OnEnable()
    {
        inputManager.ChangeWeaponButtonClicked += _playerInventoryHolder.ChangeWeapon;
    }

    private void OnDisable()
    {
        inputManager.ChangeWeaponButtonClicked -= _playerInventoryHolder.ChangeWeapon;
    }

    private void Update()
    {
        ProcessInput(inputManager.ShootingDirectionVector3);
    }

    private void ProcessInput(Vector3 direction)
    {
        if (direction.magnitude > Mathf.Epsilon)
        {
            _selfTransform.rotation = Quaternion.Lerp(_selfTransform.rotation, Quaternion.LookRotation(direction), _rotationSpeed * Time.deltaTime);

            if (!_currentWeapon.IsShooting && direction.magnitude > .5f)
                _currentWeapon.StartShoot();

        }
        else
        {
            _selfTransform.localRotation = Quaternion.Lerp(_selfTransform.localRotation, Quaternion.identity, _rotationSpeed * Time.deltaTime);

            if (_currentWeapon.IsShooting)
                _currentWeapon.StopShoot();
        }
    }
}

