using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryHolder : MonoBehaviour
{
    [SerializeField] private WeaponsData _weaponsData;

    private Inventory _inventory;
    private List<Weapon> _equipedWeapons = new List<Weapon>();
    
    public Weapon CurrentWeapon { get; private set; }

    private void Awake()
    {
        _inventory = Inventory.Load();
        SpawnWeapons();
        ChangeWeapon();
    }

    private void SpawnWeapons()
    {
        foreach(var weapon in _inventory.EquipedWeapons)
        {
            _equipedWeapons.Add(Instantiate(weapon.Prefab, transform.position, Quaternion.identity, transform));
            _equipedWeapons[_equipedWeapons.Count - 1].Disable();
        }
    }

    public void ChangeWeapon()
    {
        if(!CurrentWeapon)
        {
            CurrentWeapon = _equipedWeapons[0];
            CurrentWeapon.Enable();
            return;
        }

        int nextWeaponIndex = _equipedWeapons.IndexOf(CurrentWeapon) + 1;

        if(nextWeaponIndex >= _equipedWeapons.Count)
        {
            nextWeaponIndex = 0;
        }

        CurrentWeapon.Disable();
        CurrentWeapon = _equipedWeapons[nextWeaponIndex];
        CurrentWeapon.Enable();
    }
}
