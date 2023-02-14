using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryHolder : MonoBehaviour
{
    [SerializeField] private ItemsData itemsData;

    private Inventory _inventory;
    private List<Weapon> _equipedWeapons = new List<Weapon>();
    
    public Weapon CurrentWeapon { get; private set; }
    public Armor CurrentArmor { get; private set; }

    private void Awake()
    {
        _inventory = Inventory.Load();
        SpawnWeapons();
        ChangeWeapon();
        SpawnArmor();
    }

    private void SpawnWeapons()
    {
        foreach(var weapon in _inventory.EquippedWeapons)
        {
            _equipedWeapons.Add(Instantiate(weapon.Prefab.GetComponent<Weapon>(), transform.position, Quaternion.identity, transform));
            _equipedWeapons[_equipedWeapons.Count - 1].Disable();
        }
    }

    private void SpawnArmor()
    {
        CurrentArmor = Instantiate(_inventory.EquippedArmor.Prefab.GetComponent<Armor>(), transform.position, Quaternion.identity, transform);
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
