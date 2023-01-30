using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryInit : MonoBehaviour
{
    [SerializeField] private List<string> _startWeapons= new List<string>();
    [SerializeField] private WeaponsData _weaponData;
    

    private void Awake()
    {
        Inventory inventory = Inventory.Load();

        if(inventory.Weapons.Count == 0)
        {
            foreach (var index in _startWeapons)
            {
                inventory.Add(_weaponData.GetWeapon(index));
                inventory.Equip(inventory.Weapons.Last());
            }

            inventory.Save();
        }
    }
}
