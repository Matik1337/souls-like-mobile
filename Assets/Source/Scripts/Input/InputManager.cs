using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    [SerializeField] private InputTypes _inputType;
    [SerializeField] private VariableJoystick _movementJoystick;
    [SerializeField] private VariableJoystick _shootingJoystick;
    [SerializeField] private Button _buttonWeaponChange;
    [SerializeField] private Button _buttonUseAbility;
    [SerializeField] private LayerMask _groundLayer;

    private Transform _cameraTransform;
    private Vector3 ToVector3(Vector2 vector) => new (vector.x, 0, vector.y);

    public Vector2 ShootingDirection => GetShootDirection();
    public Vector2 MoveDirection => GetMoveDirection();
    public Vector3 ShootingDirectionVector3 => ToVector3(ShootingDirection);
    public Vector3 MoveDirectionVector3 => ToVector3(MoveDirection);
    public Vector3 MouseHitPoint { get; private set; }
    public InputTypes CurrentType => _inputType;
    public bool ShiftPressed { get; private set; }
    public bool ShootingActive { get; private set; }
    
    public event UnityAction ChangeWeaponButtonClicked;
    public event UnityAction AbilityButtonClicked;

    private void Awake()
    {
        ShootingActive = false;
    }

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

        if (_inputType == InputTypes.Standalone)
        {
            _movementJoystick.gameObject.SetActive(false);
            _shootingJoystick.gameObject.SetActive(false);
            _buttonUseAbility.gameObject.SetActive(false);
            _buttonWeaponChange.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (_inputType == InputTypes.Standalone)
        {
            if(Input.GetKeyDown(KeyCode.Q))
                AbilityButtonClicked?.Invoke();
            if(Input.GetKeyDown(KeyCode.F))
                ChangeWeaponButtonClicked?.Invoke();
            
            ShiftPressed = Input.GetKey(KeyCode.LeftShift);
            ShootingActive = Input.GetMouseButton(0) && !ShiftPressed;
        }
    }

    private Vector2 GetMoveDirection()
    {
        if (_inputType == InputTypes.Mobile)
            return RotateDirection(_movementJoystick.Direction, _cameraTransform.rotation.y);
        
        if (_inputType == InputTypes.Standalone)
            return RotateDirection(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")),
                _cameraTransform.rotation.y);

        throw new Exception("No current Input Type realisation");
    }

    private Vector2 GetShootDirection()
    {
        if (_inputType == InputTypes.Mobile)
        {
            Vector2 direction = RotateDirection(_shootingJoystick.Direction, _cameraTransform.rotation.y);
            ShootingActive = direction != Vector2.zero;

            return direction;
        }

        if (_inputType == InputTypes.Standalone)
        {
            return GetMouseDeviation();
        }

        throw new Exception("No current Input Type realisation");
    }

    private Vector2 GetMouseDeviation()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (!ShiftPressed && Physics.Raycast(ray, out RaycastHit hit, 100, _groundLayer))
        {
            MouseHitPoint = hit.point;
            
            Vector3 direction = MouseHitPoint - transform.position;
            
            return new Vector2(direction.x, direction.z);
        }
        
        return Vector2.zero;
    }
    
    private Vector2 RotateDirection(Vector2 direction, float angle)
    {
        if (direction == Vector2.zero)
            return direction;
        
        float angleRad = -angle * 2;
        Vector2 result = direction;

        result.x = direction.x * Mathf.Cos(angleRad) - direction.y * Mathf.Sin(angleRad);
        result.y = direction.x * Mathf.Sin(angleRad) + direction.y * Mathf.Cos(angleRad);

        if (result.magnitude > 1)
            result.Scale(new (1 / result.magnitude, 1/ result.magnitude));

        return result;
    }

    private void OnAbilityButtonClicked() => AbilityButtonClicked?.Invoke();
    private void OnWeaponChangeButtonClicked() => ChangeWeaponButtonClicked?.Invoke();
}
