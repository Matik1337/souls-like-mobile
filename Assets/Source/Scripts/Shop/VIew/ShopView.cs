using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShopView : MonoBehaviour
{
    [SerializeField] private ItemType _targetItemType;
    [SerializeField] private ShopPositionsDataBase _shopPositions;
    [SerializeField] private ItemsData _itemsData;
    [SerializeField] private Transform _cellsParent;
    [SerializeField] private ShopPositionCell _cellPrefab;
    [SerializeField] private MenuInventoryHolder _inventoryHolder;

    private IEnumerable<ItemInfo> _targetItems;
    private IEnumerable<ShopPosition> _targetPositions;

    private List<ShopPositionCell> _cells;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _targetItems = _itemsData.Items.Where(i => i.ItemType == _targetItemType);
        _targetPositions = _shopPositions.ShopPositions.Where(p => _targetItems.Any(i => i.ID == p.RewardID));
        _cells = new List<ShopPositionCell>();
        
        foreach (var position in _targetPositions)
        {
            ShopPositionCell cell = Instantiate(_cellPrefab, _cellsParent);

            if (position.ResourceID >= 0 && position.TradeItemID >= 0)
            {
                cell.Init(position.SoftCurrencyCost, 
                    _itemsData.GetItem(position.RewardID), 
                    _itemsData.GetItem(position.ResourceID), 
                    _itemsData.GetItem(position.TradeItemID), 
                    _inventoryHolder.CurrentSoftCurrency >= position.SoftCurrencyCost);
                continue;
            }
            
            if (position.ResourceID >= 0)
            {
                cell.Init(position.SoftCurrencyCost, 
                    _itemsData.GetItem(position.RewardID), 
                    _itemsData.GetItem(position.ResourceID),
                    _inventoryHolder.CurrentSoftCurrency >= position.SoftCurrencyCost);
                continue;
            }
            
            if (position.TradeItemID >= 0)
            {
                cell.Init(_itemsData.GetItem(position.RewardID), 
                    position.SoftCurrencyCost,
                    _itemsData.GetItem(position.ResourceID),
                    _inventoryHolder.CurrentSoftCurrency >= position.SoftCurrencyCost);
                continue;
            }
            
            cell.Init(position.SoftCurrencyCost, 
                _itemsData.GetItem(position.RewardID),
                _inventoryHolder.CurrentSoftCurrency >= position.SoftCurrencyCost);
        }

        foreach (var cell in _cells)
        {
            cell.Bought += OnBought;
        }
    }

    private void OnBought(ItemInfo item)
    {
        
    }
}
