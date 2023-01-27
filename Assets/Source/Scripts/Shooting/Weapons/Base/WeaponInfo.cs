using UnityEngine;

public class WeaponInfo : MonoBehaviour
{
    [SerializeField] private string _name;
    [SerializeField] private int _cost;
    [SerializeField] private Sprite _icon;
    [SerializeField] private WeaponQuality _quality;
    [SerializeField] private Weapon _prefab;
    
    public string Name => _name;
    public int Cost => _cost;
    public Sprite Icon => _icon;
    public WeaponQuality Quality => _quality;
    public Weapon Prefab => _prefab;
}

public enum WeaponQuality
{
    Regular, Rare, Epic, Legendary
}