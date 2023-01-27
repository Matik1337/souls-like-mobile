using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(order = 51, fileName = nameof(WeaponsData), menuName = nameof(WeaponsData))]
public class WeaponsData : ScriptableObject
{
    [SerializeField] private List<WeaponInfo> _weapons;

    public IEnumerable<WeaponInfo> Weapons => _weapons;

    public WeaponInfo GetWeaponByName(string weaponName)
    {
        return _weapons.First(w => w.Name == weaponName);
    }
}
