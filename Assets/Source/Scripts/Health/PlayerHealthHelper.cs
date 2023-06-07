using UnityEngine;

public class PlayerHealthHelper : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private GameInventoryHolder _inventory;

    public void TakeDamage(int damage, EnemyClass enemyClass)
    {
        if (_inventory.CurrentArmor)
            damage = _inventory.CurrentArmor.GetDamageValue(damage, enemyClass);

        _health.TakeDamage(damage);
    }
}
