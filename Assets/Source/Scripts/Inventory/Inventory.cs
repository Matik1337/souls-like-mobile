using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Inventory
{
    [SerializeField] private List<WeaponInfo> _weapons = new List<WeaponInfo>();
    [SerializeField] private List<WeaponInfo> _equipenWeapons = new List<WeaponInfo>();

    [SerializeField] private int _maxEquipedWeaponsCount = 2;

    public List<WeaponInfo> Weapons => _weapons;
    public List<WeaponInfo> EquipedWeapons => _equipenWeapons;
    public static string SaveKey = nameof(Inventory);

    public int MaxEquipedWeaponsCount => _maxEquipedWeaponsCount;

    public void Add(WeaponInfo weapon)
    {
        if(!_weapons.Contains(weapon)) 
            _weapons.Add(weapon);
    }

    public void Remove(WeaponInfo weapon)
    {
        if (_weapons.Contains(weapon))
            _weapons.Remove(weapon);
        
        Dequip(weapon);
    }

    public void Equip(WeaponInfo weapon)
    {
        if(_weapons.Contains(weapon) && ! _equipenWeapons.Contains(weapon))
        {
            if(_equipenWeapons.Count < MaxEquipedWeaponsCount)
            {
                _equipenWeapons.Add(weapon);
            }
        }
    }

    public void Dequip(WeaponInfo weapon)
    {
        if (_equipenWeapons.Contains(weapon))
        {
            _equipenWeapons.Remove(weapon);
        }
    }

    public static Inventory Load()
    {
        if(PlayerPrefs.HasKey(SaveKey))
        {
            return JsonUtility.FromJson<Inventory>(PlayerPrefs.GetString(SaveKey));
        }
        else
        {
            return new Inventory();
        }
    }

    public void Save()
    {
        PlayerPrefs.SetString(SaveKey, JsonUtility.ToJson(this));
    }
}
