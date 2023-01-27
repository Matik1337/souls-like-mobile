using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BulletsPool : MonoBehaviour
{
    [SerializeField] private Bullet _prefab;
    [SerializeField] private int _size;

    private List<Bullet> _bullets;

    private void Awake()
    {
        SpawnBullets();
    }

    public void Shoot(Vector3 shootPoint, Vector3 direction)
    {
        Bullet bullet = GetFreeOrFirstBullet();

        bullet.transform.position = shootPoint;
        bullet.gameObject.SetActive(true);
        bullet.Run(direction);
    }

    private void SpawnBullets()
    {
        _bullets = new List<Bullet>();

        for(int i = 0; i < _size; i++)
        {
            Bullet bullet = Instantiate(_prefab, Vector3.zero, Quaternion.identity);

            bullet.gameObject.SetActive(false);
            _bullets.Add(bullet);
        }
    }

    private Bullet GetFreeOrFirstBullet()
    {
        if(_bullets.Any(b => !b.IsActive))
        {
            return _bullets.First(b => !b.IsActive);
        }
        else
        {
            Bullet bullet = _bullets.OrderBy(b => b.RemainingDistance).First();

            bullet.ResetState();
            return bullet;
        }
    }
}
