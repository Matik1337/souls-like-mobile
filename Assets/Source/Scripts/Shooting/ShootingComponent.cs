using System;
using UnityEngine;

public class ShootingComponent : MonoBehaviour
{
    [SerializeField] private Transform _selfTransform;
    [SerializeField] private float _rotationSpeed;

    private Weapon _currentWeapon;
    private float _minDirectionMagnitude = .5f;
    
    public void ProcessInput(Vector3 direction, bool canShoot)
    {
        if (!_currentWeapon)
            throw new Exception("Current weapon is not initialized");
        
        if (direction.magnitude > Mathf.Epsilon)
        {
            _selfTransform.rotation = Quaternion.Lerp(_selfTransform.rotation, Quaternion.LookRotation(direction), _rotationSpeed * Time.deltaTime);

            if (canShoot && !_currentWeapon.IsShooting && direction.magnitude > _minDirectionMagnitude)
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

    public void SetCurrentWeapon(Weapon weapon)
    {
        if(_currentWeapon && _currentWeapon.IsShooting)
            _currentWeapon.StopShoot();

        _currentWeapon = weapon;
    }
}
