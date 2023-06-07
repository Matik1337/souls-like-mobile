using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ShopPosition
{
    [SerializeField] private int _rewardID;
    [SerializeField] private int _softCurrencyCost;
    [SerializeField] private int _resourceID;
    [SerializeField] private int _tradeItemID;

    public int RewardID => _rewardID;
    public int SoftCurrencyCost => _softCurrencyCost;
    public int ResourceID => _resourceID;
    public int TradeItemID => _tradeItemID;
}
