using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ShopPositions", menuName = "ShopPositions", order = 51)]
public class ShopPositionsDataBase : ScriptableObject
{
    [SerializeField] private List<ShopPosition> _shopPositions;

    public IEnumerable<ShopPosition> ShopPositions => _shopPositions;
}
