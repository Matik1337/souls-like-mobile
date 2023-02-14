using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(order = 51, fileName = nameof(ItemsData), menuName = nameof(ItemsData))]
public class ItemsData : ScriptableObject
{
    [SerializeField] private List<ItemInfo> _weapons;

    public IEnumerable<ItemInfo> Weapons => _weapons;

    public ItemInfo GetWeapon(string weaponName)
    {
        return _weapons.First(w => w.Name == weaponName);
    }
}
