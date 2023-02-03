using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : MonoBehaviour
{
    [SerializeField] private int _bulletProof;
    [SerializeField] private int _tearProof;

    public int BulletProof => _bulletProof;
    public int TearProof => _tearProof;

    public int GetDamageValue(int damage, EnemyClass enemyClass)
    {
        switch (enemyClass)
        {
            case EnemyClass.Creature:
                return _tearProof * damage;
            case EnemyClass.Human: 
                return _bulletProof * damage;
            default: 
                return damage;
        }
    }
}
