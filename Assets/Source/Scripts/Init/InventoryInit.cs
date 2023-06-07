using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryInit : MonoBehaviour
{
    [SerializeField] private List<string> _startWeapons= new List<string>();
    [SerializeField] private string _startArmor;
    [SerializeField] private ItemsData itemData;
    

    private void Awake()
    {
        Inventory inventory = Inventory.Load();

        if(inventory.Items.Count == 0)
        {
            foreach (var index in _startWeapons)
            {
                inventory.Add(itemData.GetItem(index));
                inventory.EquipWeapon(inventory.Items.Last());
            }
            
            inventory.Add(itemData.GetItem(_startArmor));
            inventory.EquipArmor(inventory.Items.Last());
            inventory.Save();
        }
    }
}
