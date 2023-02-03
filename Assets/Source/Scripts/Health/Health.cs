using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private int _startHealth;
    [SerializeField] private bool _needRegenerate;
    [SerializeField] private int _regenerationSpeed;
    
    public int CurrentValue {get; private set;}
    public event UnityAction ValueChanged;

    private void Awake()
    {
        CurrentValue = _startHealth;

        if(_needRegenerate)
            StartCoroutine(Regenerate());
    }

    public void TakeDamage(int damage)
    {
        CurrentValue = Mathf.Clamp(CurrentValue - damage, 0, _startHealth);
        ValueChanged?.Invoke();
    }

    private IEnumerator Regenerate()
    {
        while(CurrentValue > 0)
        {
            if(CurrentValue < _startHealth)
            {
                CurrentValue++;
                ValueChanged?.Invoke();
            }
                
            yield return new WaitForSeconds(1 / _regenerationSpeed);
        }
    }
}
