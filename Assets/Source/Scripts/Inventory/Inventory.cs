using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class Inventory
{
    [SerializeField] private List<ItemInfo> _items = new List<ItemInfo>();
    [SerializeField] private List<ItemInfo> equippedWeapons = new List<ItemInfo>();
    [SerializeField] private ItemInfo _equippedArmor;
    [SerializeField] private int maxEquippedWeaponsCount = 2;

    public List<ItemInfo> Items => _items;
    public List<ItemInfo> EquippedWeapons => equippedWeapons;
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
        
        //Disequip(item);
    }

    public void EquipWeapon(ItemInfo item)
    {
        if(item.ItemType == ItemType.Weapon && _items.Contains(item) && ! equippedWeapons.Contains(item))
        {
            if(equippedWeapons.Count < MaxEquippedWeaponsCount)
            {
                equippedWeapons.Add(item);
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
        if (item.ItemType == ItemType.Weapon && equippedWeapons.Contains(item))
        {
            equippedWeapons.Remove(item);
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
