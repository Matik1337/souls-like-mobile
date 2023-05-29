using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected BulletsPool BulletsPool;
    [SerializeField] private float _frequency;
    [SerializeField] protected float Spreading;
    [SerializeField] protected float SpreadingGrowth;
    [SerializeField] protected float MaxSpreading;
    [SerializeField] protected Transform ShootPoint;
    [SerializeField] protected Transform DirectionPoint;
    
    public bool IsShooting { get; private set; }
    public float Frequency => _frequency;
    
    private Coroutine _coroutine;

    public virtual void StartShoot()
    {
        if (!IsShooting)
        {
            IsShooting = true;
            
            if(_coroutine == null)
                _coroutine = StartCoroutine(Shoot());
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
        float currentSpreading = Spreading;
        
        while (IsShooting)
        {
            BulletsPool.Shoot(ShootPoint.position, GetNoisedDirection(DirectionPoint.position, currentSpreading));
            
            if(currentSpreading < MaxSpreading)
                currentSpreading += SpreadingGrowth;
            
            yield return new WaitForSeconds(1 / Frequency);
        }

        _coroutine = null;
    }

    protected virtual Vector3 GetNoisedDirection(Vector3 direction, float spreading)
    {
        if (spreading == 0)
            return direction;
        
        float angleRad = Random.Range(-spreading, spreading) * Mathf.Deg2Rad;
        Vector3 result = direction;
        
        result.x = direction.x * Mathf.Cos(angleRad) - direction.z * Mathf.Sin(angleRad);
        result.z = direction.x * Mathf.Sin(angleRad) + direction.z * Mathf.Cos(angleRad);

        return result;
    }
}
