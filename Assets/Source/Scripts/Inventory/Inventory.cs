using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class Inventory
{
    [SerializeField] private List<ItemInfo> _items = new List<ItemInfo>();
    [SerializeField] private List<ItemInfo> _equippedWeapons = new List<ItemInfo>();
    [SerializeField] private ItemInfo _equippedArmor;
    [SerializeField] private int maxEquippedWeaponsCount = 2;

    public List<ItemInfo> Items => _items;
    public List<ItemInfo> EquippedWeapons => _equippedWeapons;
    public ItemInfo EquippedArmor => _equippedArmor;
    public static string SaveKey = nameof(Inventory);

    public int MaxEquippedWeaponsCount => maxEquippedWeaponsCount;

    public void Add(ItemInfo item)
    {
        if (!_items.Contains(item))
            _items.Add(item);
    }

    public void Add(ItemInfo item, int count)
    {
        if (!_items.Any(i => i.Name == item.Name))
            _items.Add(item);
        
        _items.First(i => i.Name == item.Name).AddCount(count);
    }

    public void Remove(ItemInfo item)
    {
        if (_items.Contains(item))
            _items.Remove(item);
        
        if(item.ItemType == ItemType.Weapon)
            DisequipWeapon(item);
    }

    public void EquipWeapon(ItemInfo item)
    {
        if(item.ItemType == ItemType.Weapon && _items.Contains(item) && ! _equippedWeapons.Contains(item))
        {
            if(_equippedWeapons.Count < MaxEquippedWeaponsCount)
            {
                _equippedWeapons.Add(item);
            }
        }
    }

    public void EquipArmor(ItemInfo item)
    {
        if(item.ItemType == ItemType.Armor && _items.Contains(item) && _equippedArmor.Name != item.Name)
        {
            _equippedArmor = item;
        }
    }
    public void DisequipWeapon(ItemInfo item)
    {
        if (item.ItemType == ItemType.Weapon && _equippedWeapons.Contains(item))
        {
            _equippedWeapons.Remove(item);
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
