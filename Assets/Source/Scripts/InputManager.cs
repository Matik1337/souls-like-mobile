using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    [SerializeField] private VariableJoystick _movementJoystick;
    [SerializeField] private VariableJoystick _shootingJoystick;
    [SerializeField] private Button _buttonWeaponChange;
    [SerializeField] private Button _buttonUseAbility;

    private Transform _cameraTransform;
    private Vector3 ToVector3(Vector2 vector) => new Vector3(vector.x, 0, vector.y);
    
    public Vector2 ShootingDirection => RotateDirection(_shootingJoystick.Direction, _cameraTransform.rotation.y);
    public Vector2 MoveDirection => RotateDirection(_movementJoystick.Direction, _cameraTransform.rotation.y);
    public Vector3 ShootingDirectionVector3 => ToVector3(ShootingDirection);
    public Vector3 MoveDirectionVector3 => ToVector3(MoveDirection);
    
    public event UnityAction ChangeWeaponButtonClicked;
    public event UnityAction AbilityButtonClicked;

    private void OnEnable()
    {
        _buttonUseAbility.onClick.AddListener(OnAbilityButtonClicked);
        _buttonWeaponChange.onClick.AddListener(OnWeaponChangeButtonClicked);
    }

    private void OnDisable()
    {
        _buttonUseAbility.onClick.RemoveListener(OnAbilityButtonClicked);
        _buttonWeaponChange.onClick.RemoveListener(OnWeaponChangeButtonClicked);
    }

    private void Start()
    {
        Camera mainCamera = Camera.main;
        
        if(mainCamera)
            _cameraTransform = mainCamera.transform;
    }

    private Vector2 RotateDirection(Vector2 direction, float angle)
    {
        float angleRad = -angle * Mathf.Deg2Rad;
        Vector2 result = direction;
        
        result.x = direction.x * Mathf.Cos(angleRad) - direction.y * Mathf.Sin(angleRad);
        result.y = direction.x * Mathf.Sin(angleRad) + direction.y * Mathf.Cos(angleRad);

        return result;
    }

    private void OnAbilityButtonClicked() => AbilityButtonClicked?.Invoke();
    private void OnWeaponChangeButtonClicked() => ChangeWeaponButtonClicked?.Invoke();
}
