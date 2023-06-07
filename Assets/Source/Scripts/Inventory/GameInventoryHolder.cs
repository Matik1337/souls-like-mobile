using System.Collections.Generic;
using UnityEngine;

public class GameInventoryHolder : MonoBehaviour
{
    [SerializeField] private ItemsData itemsData;

    private Inventory _inventory;
    private List<Weapon> _equippedWeapons = new List<Weapon>();
    private SoftCurrencyHolder _softCurrencyHolder = new SoftCurrencyHolder();
    
    public Weapon CurrentWeapon { get; private set; }
    public Armor CurrentArmor { get; private set; }

    private void Awake()
    {
        _inventory = Inventory.Load();
        _softCurrencyHolder.Load();
        
        SpawnWeapons();
        ChangeWeapon();
        SpawnArmor();
    }

    private void SpawnWeapons()
    {
        foreach(var weapon in _inventory.EquippedWeapons)
        {
            _equippedWeapons.Add(Instantiate(weapon.Prefab.GetComponent<Weapon>(), transform.position, Quaternion.identity, transform));
            _equippedWeapons[_equippedWeapons.Count - 1].Disable();
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
            CurrentWeapon = _equippedWeapons[0];
            CurrentWeapon.Enable();
            return;
        }

        int nextWeaponIndex = _equippedWeapons.IndexOf(CurrentWeapon) + 1;

        if(nextWeaponIndex >= _equippedWeapons.Count)
        {
            nextWeaponIndex = 0;
        }

        CurrentWeapon.Disable();
        CurrentWeapon = _equippedWeapons[nextWeaponIndex];
        CurrentWeapon.Enable();
    }

    public void AddResource(ItemInfo resource)
    {
        _inventory.Add(resource);
        _inventory.Save();
    }

    public void AddSoftCurrency(int value)
    {
        _softCurrencyHolder.Add(value);
        _softCurrencyHolder.Save();
    }

    public bool TryRemoveSoftCurrency(int value)
    {
        bool result = _softCurrencyHolder.TryRemove(value);
        
        _softCurrencyHolder.Save();
        
        return result;
    }
}
