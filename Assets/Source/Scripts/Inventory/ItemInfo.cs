using System;
using UnityEngine;

[Serializable]
public struct ItemInfo
{
    [SerializeField] private string _name;
    [SerializeField] private int _cost;
    [SerializeField] private Sprite _icon;
    [SerializeField] private ItemType _itemType;
    [SerializeField] private ItemQuality _quality;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private int _count;
    [SerializeField] private bool _loskCount;
    
    public string Name => _name;
    public int Cost => _cost;
    public Sprite Icon => _icon;
    public ItemQuality Quality => _quality;
    public ItemType ItemType => _itemType;
    public GameObject Prefab => _prefab;
    public int Count => _count;

    public void AddCount(int delta)
    {
        if (!_loskCount && _count + delta >= 0)
            _count += delta;
    }
}

public enum ItemQuality
{
    Regular, Rare, Epic, Legendary
}

public enum ItemType
{
    Weapon, Armor, Artifact, Resource
}