using UnityEngine;

public class MenuInventoryHolder : MonoBehaviour
{
    private Inventory _inventory;
    private SoftCurrencyHolder _softCurrencyHolder;

    public int CurrentSoftCurrency => _softCurrencyHolder.Value;

    private void Awake()
    {
        _inventory = Inventory.Load();
        _softCurrencyHolder = new SoftCurrencyHolder();
        _softCurrencyHolder.Load();
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
    
    public void AddItem(ItemInfo itemInfo)
    {
        _inventory.Add(itemInfo);
        _inventory.Save();
    }

    public void RemoveItem(ItemInfo itemInfo)
    {
        _inventory.Remove(itemInfo);
        _inventory.Save();
    }

    public void EquipArmour(ItemInfo itemInfo)
    {
        _inventory.EquipArmor(itemInfo);
        _inventory.Save();
    }

    public void EquipWeapon(ItemInfo itemInfo)
    {
        _inventory.EquipWeapon(itemInfo);
        _inventory.Save();
    }

    public void DisequipWeapon(ItemInfo itemInfo)
    {
        _inventory.DisequipWeapon(itemInfo);
        _inventory.Save();
    }
}
