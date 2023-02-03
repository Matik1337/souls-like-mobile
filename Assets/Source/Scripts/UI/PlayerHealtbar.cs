using UnityEngine;
using UnityEngine.UI;

public class PlayerHealtbar : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private Slider _slider;
    [SerializeField] private float _changeSpeed;

    private void Start()
    {
        _slider.maxValue = _health.CurrentValue;
        _slider.value = _health.CurrentValue;
    }

    private void Update()
    {
        if (_slider.value != _health.CurrentValue)
        {
            _slider.value = Mathf.MoveTowards(_slider.value, _health.CurrentValue, _changeSpeed * Time.deltaTime);
        }
    }
}
