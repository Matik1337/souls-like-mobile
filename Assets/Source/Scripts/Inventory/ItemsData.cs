using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(order = 51, fileName = nameof(ItemsData), menuName = nameof(ItemsData))]
public class ItemsData : ScriptableObject
{
    [FormerlySerializedAs("_weapons")] [SerializeField] private List<ItemInfo> _items;

    public IEnumerable<ItemInfo> Items => _items;

    public ItemInfo GetItem(string weaponName)
    {
        return _items.First(w => w.Name == weaponName);
    }

    public ItemInfo GetItem(int id)
    {
        return _items[id];
    }
}
