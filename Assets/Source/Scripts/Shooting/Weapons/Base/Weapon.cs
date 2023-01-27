using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected BulletsPool BulletsPool;
    [SerializeField] private float _frequency;
    [SerializeField] protected float Spreading;
    [SerializeField] protected Transform ShootPoint;
    [SerializeField] protected Transform DirectionPoint;
    
    public bool IsShooting { get; private set; }
    public float Frequency => _frequency;

    public virtual void StartShoot()
    {
        if (!IsShooting)
        {
            IsShooting = true;
            StartCoroutine(Shoot());
        }
    }

    public virtual void StopShoot()
    {
        IsShooting = false;
    }

    public virtual void Enable()
    {
        enabled = true;
        gameObject.SetActive(true);
    }

    public virtual void Disable()
    {
        enabled = false;
        gameObject.SetActive(false);
    }

    protected virtual IEnumerator Shoot()
    {
        while (IsShooting)
        {
            BulletsPool.Shoot(ShootPoint.position, DirectionPoint.position);
                        
            yield return new WaitForSeconds(1 / Frequency);
        }
    }

    protected virtual Vector3 GetNoisedDirection(Vector3 direction)
    {
        if (Spreading == 0)
            return direction;
        
        float angleRad = Random.Range(-Spreading, Spreading) * Mathf.Deg2Rad;
        Vector3 result = direction;
        
        result.x = direction.x * Mathf.Cos(angleRad) - direction.z * Mathf.Sin(angleRad);
        result.z = direction.x * Mathf.Sin(angleRad) + direction.z * Mathf.Cos(angleRad);

        return result;
    }
}
